using UnityEngine;

namespace Assets.Source
{
    [DefaultExecutionOrder(-4000)]
    public class CompositeRoot : MonoBehaviour
    {
        private const int FrameRateLock = 60;

        [SerializeField] private DIContainer _container;
        private GameObjectsPlayBackHandler _gameObjectsPlayBackHandler;
        private CollectablesSpawnerPresenter _collectablesSpawner;
        private EnemySpawnerPresenter _enemiesSpawner;
        private WeaponPresenter _weapon;
        private PlayerPresenter _player;
        private GameState _gameState;
        private GameUIPresenter _gameUI;
        private IFactory _factory;

        private void OnEnable()
        {
            Application.targetFrameRate = FrameRateLock;
            _factory = new Factory();

            Create();
            Initialize();
            StartGame();
        }

        private void Create()
        {
            _gameState = new();
            _gameObjectsPlayBackHandler = new();
            _gameUI = _container.GameUIPresenter;

            _weapon = CreateWeapon();
            _player = CreatePlayer();
            _enemiesSpawner = CreateEnemiesSpawner();
            _collectablesSpawner = CreateCollectableSpawner();

            _gameUI.Construct(_container.InputHandler, _container.SpellsConfig.SpecialSpells, _container.SpellsConfig.DefaultSpells);
            _weapon.Construct(_container.WeaponConfig);
            _player.Construct(_weapon, _container.PlayerConfig, _container.Camera, _container.AimInWorldSpaceOrientation.Aim, _gameUI.HealthsBar,
                             _gameUI.ExperionceBar, _gameState, _gameUI.PlayerLevelView, _gameUI.SpellMenuOpen);
            _collectablesSpawner.Construct();
            _enemiesSpawner.Construct(_container.SpawnerConfig, _player, _gameState, _collectablesSpawner.Spawner);
        }

        private void Initialize()
        {
            _player.Initialize();
            _gameUI.Initialize(_player.PlayerUpgradeHadler, _gameObjectsPlayBackHandler);
            _gameObjectsPlayBackHandler.Initialize(_weapon.Weapon, _player, _enemiesSpawner.Spawner, _weapon.BulletsPoolPresenter.BulletsPool.Bullets,
                _enemiesSpawner.Spawner.AliveEnemy, _collectablesSpawner.Spawner.Collectables);
            _gameState.Initialize(_enemiesSpawner.Spawner, _player.PlayerStateTransition, _container.GameUIPresenter.GameStateWindowsUI);
            _container.AimInWorldSpaceOrientation.Initialize(_player, _container.Camera);
            _container.InputHandler.Initialize(_gameObjectsPlayBackHandler, _player.PlayerMover, _player.PlayerAnimator, _player.PlayerAttacker);
            CameraInitialize(_player.PlayerTransform);
        }

        private void StartGame()
        {
            _enemiesSpawner.StartSpawn();
            _container.GameUIPresenter.OnStarted();
        }

        private PlayerPresenter CreatePlayer()
        {
            PlayerPresenter player = (PlayerPresenter)_factory.Create(_container.PlayerPresenterPrefab, transform.position, null);
            return player;
        }

        private WeaponPresenter CreateWeapon()
        {
            WeaponPresenter weapon = (WeaponPresenter)_factory.Create(_container.WeaponPresenterPrefab, transform.position, null);
            return weapon;
        }

        private EnemySpawnerPresenter CreateEnemiesSpawner()
        {
            EnemySpawnerPresenter spawner = (EnemySpawnerPresenter)_factory.Create(_container.EnemiesSpawnerPrefab, transform.position, null);
            return spawner;
        }

        private CollectablesSpawnerPresenter CreateCollectableSpawner()
        {
            CollectablesSpawnerPresenter spawner = (CollectablesSpawnerPresenter)_factory.Create(_container.CollectablesSpawnerPrefab, transform.position, null);
            return spawner;
        }

        private void CameraInitialize(Transform playerTransform) => _container.CameraInitializer.Initialize(playerTransform);
    }
}
