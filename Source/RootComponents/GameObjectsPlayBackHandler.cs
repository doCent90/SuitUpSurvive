using System.Collections.Generic;

namespace Assets.Source
{
    public class GameObjectsPlayBackHandler : IGameObjectsPlayBackHandler
    {
        private IGameObjectsPlayBackHandler _weapon;
        private IGameObjectsPlayBackHandler _player;
        private IGameObjectsPlayBackHandler _enemySpawner;
        private IReadOnlyList<Collectable> _collectables;
        private IReadOnlyList<BulletPresenter> _bulletsPresenters;
        private IReadOnlyList<IGameObjectsPlayBackHandler> _enemyPlayBackHandlers;
        private readonly List<IGameObjectsPlayBackHandler> _gameTimeHandlers = new();

        public void Initialize(IGameObjectsPlayBackHandler weapon, IGameObjectsPlayBackHandler player, IGameObjectsPlayBackHandler enemySpawner, IReadOnlyList<BulletPresenter> bulletsPresenters,
            IReadOnlyList<IGameObjectsPlayBackHandler> enemySpawnerHolder, IReadOnlyList<Collectable> collectables)
        {
            _weapon = weapon;
            _player = player;
            _enemySpawner = enemySpawner;
            _collectables = collectables;
            _bulletsPresenters = bulletsPresenters;
            _enemyPlayBackHandlers = enemySpawnerHolder;

            _gameTimeHandlers.Add(_weapon);
            _gameTimeHandlers.Add(_player);
            _gameTimeHandlers.Add(_enemySpawner);
        }

        public void Resume()
        {
            SetResumeForObjects(_gameTimeHandlers);
            SetResumeForObjects(_enemyPlayBackHandlers);
            SetResumeForObjects(_bulletsPresenters);
            SetResumeForObjects(_collectables);
        }

        public void Pause()
        {
            SetPauseForObjects(_gameTimeHandlers);
            SetPauseForObjects(_enemyPlayBackHandlers);
            SetPauseForObjects(_bulletsPresenters);
            SetPauseForObjects(_collectables);
        }

        private void SetResumeForObjects(IReadOnlyList<IGameObjectsPlayBackHandler> gameObjects)
        {
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Resume();
        }

        private void SetPauseForObjects(IReadOnlyList<IGameObjectsPlayBackHandler> gameObjects)
        {
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Pause();
        }
    }
}
