using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    public class EnemySpawner : IEnemyDeadState, IEnemyCreator, IGameObjectsPlayBackHandler, IEnemySpawner
    {
        private const float StartWaitTimeSec = 2f;
        private const float WaitBetweenSpawnTimeSec = 2f;

        private readonly SpawnedObjectPlacer _spawnedObjectPlacer;
        private readonly PlayerPresenter _player;
        private readonly Transform _containerAlive;
        private readonly Transform _containerDead;
        private readonly ISpawnerGameState _gameState;
        private readonly ICollectablesSpawner _collectablesSpawner;

        private readonly ICoroutineRunner _coroutine;
        private readonly IReadOnlyList<EnemyPresenterPrefabsList> _enemyPrefabsList;
        private readonly List<EnemyPresenter> _enemiesAlive = new();
        private readonly List<IGameObjectsPlayBackHandler> _enemiesDead = new();

        private bool _isGameOnPause = false;

        public IReadOnlyList<IGameObjectsPlayBackHandler> AliveEnemy => _enemiesAlive;

        public EnemySpawner(EnemySpawnerConfig config, ICoroutineRunner coroutine, Transform containerAlive,
            Transform containerDead, PlayerPresenter player, SpawnedObjectPlacer spawnedObjectPlacer, ISpawnerGameState gameState, ICollectablesSpawner collectablesSpawner)
        {
            _player = player;
            _coroutine = coroutine;
            _gameState = gameState;
            _containerDead = containerDead;
            _containerAlive = containerAlive;
            _enemyPrefabsList = config.EnemyPrefabsList;
            _collectablesSpawner = collectablesSpawner;
            _spawnedObjectPlacer = spawnedObjectPlacer;
        }

        public void Resume() => _isGameOnPause = false;

        public void Pause() => _isGameOnPause = true;

        public void StartCreating() => _coroutine.StartCoroutine(Creating());

        public void AddDeadEnemy(IEnemyPresenter enemy, IGameObjectsPlayBackHandler playBackHandler)
        {
            enemy.Transform.SetParent(_containerDead);
            _enemiesDead.Add(playBackHandler);
            _enemiesAlive.Remove((EnemyPresenter)playBackHandler);
        }

        public void OnPlayerDead()
        {
            foreach (EnemyPresenter enemy in _enemiesAlive)
                enemy.Enemy.OnPlayerDead();
        }

        private IEnumerator Creating()
        {
            float waitSpawnTime = WaitBetweenSpawnTimeSec;
            var onPause = new WaitWhile(() => _isGameOnPause);
            var startWait = new WaitForSecondsRealtime(StartWaitTimeSec);
            var betweenSpawnWait = new WaitForSecondsRealtime(waitSpawnTime);
            var lastEnemyAliveWait = new WaitWhile(() => _enemiesAlive.Count != 0);

            yield return startWait;

            for (int nextList = 0; nextList < _enemyPrefabsList.Count; nextList++)
            {
                waitSpawnTime /= _enemyPrefabsList[nextList].EnemyPrefabs.Count;

                foreach (EnemyPresenter enemy in _enemyPrefabsList[nextList].EnemyPrefabs)
                {
                    yield return onPause;
                    EnemyPresenter spawned = Object.Instantiate(enemy, _containerAlive);
                    AddAliveEnemy(spawned);
                    spawned.Construct(_player, this, _collectablesSpawner);
                    _spawnedObjectPlacer.SetPosition(spawned);
                    yield return betweenSpawnWait;
                }
            }

            yield return lastEnemyAliveWait;
            _gameState.OnPlayerWin();
        }

        private void AddAliveEnemy(EnemyPresenter enemy) => _enemiesAlive.Add(enemy);
    }
}
