using UnityEngine;

namespace Assets.Source
{
    public class EnemySpawnerPresenter : Presenter, ICoroutineRunner
    {
        [SerializeField] private Transform _containerAlive;
        [SerializeField] private Transform _containerDead;
        [SerializeField] private SpawnedObjectPlacer _spawnedObjectPlacer;

        public EnemySpawner Spawner { get; private set; }

        public void Construct(EnemySpawnerConfig config, PlayerPresenter player, ISpawnerGameState gameState, ICollectablesSpawner collectablesSpawner)
            => Spawner = new EnemySpawner(config, this, _containerAlive, _containerDead, player, _spawnedObjectPlacer, gameState, collectablesSpawner);

        public void StartSpawn() => Spawner.StartCreating();
    }
}
