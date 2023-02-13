using UnityEngine;

namespace Assets.Source
{
    [RequireComponent(typeof(PlayerUpgradeHandler))]
    public class PlayerPresenter : CharacterPresenter, IPlayerPresenter, IGameObjectsPlayBackHandler
    {
        [SerializeField] private Transform _weaponPoint;
        [SerializeField] private PlayerCollectTrigger _playerCollectTrigger;
        [Header("Particles")]
        [SerializeField] private ParticleSystem _money;
        [SerializeField] private ParticleSystem _trail;
        [SerializeField] private ParticleSystem _attackFx;

        private Camera _camera;
        private Transform _aim;
        private PlayerState _playerState;
        private PlayerConfig _playerConfig;
        private WeaponPresenter _weaponPresenter;
        private PlayerUpgradeHandler _playerUpgradeHandler;

        public Transform PlayerTransform => transform;
        public PlayerFX PlayerFX { get; private set; }
        public PlayerMover PlayerMover { get; private set; }
        public PlayerRotater PlayerRotater { get; private set; }
        public PlayerAnimator PlayerAnimator { get; private set; }
        public PlayerAttacker PlayerAttacker { get; private set; }
        public IPlayerDamagable PlayerDamagable { get; private set; }
        public IPlayerUpgradeHadler PlayerUpgradeHadler { get; private set; }
        public IPlayerTransition PlayerStateTransition => _playerState;

        public void Construct(WeaponPresenter weaponPresenter, PlayerConfig playerConfig, Camera camera, Transform aim,
            IBar heathlsBar, IBar experinceBar, IPlayerGameState gameState, IPlayerLevelView playerLevelView, ISpellMenuOpen spellMenuOpen)
        {
            _weaponPresenter = weaponPresenter;
            _playerConfig = playerConfig;
            _camera = camera;
            _aim = aim;

            PlayerFX = new PlayerFX(_money, _trail, _attackFx);
            PlayerMover = new PlayerMover(Agent, _playerConfig.Speed);
            PlayerRotater = new PlayerRotater(_aim, Model);
            PlayerAnimator = new PlayerAnimator(PlayerFX, _camera, Model, Animator);
            PlayerAttacker = new PlayerAttacker(_weaponPresenter.Weapon, PlayerAnimator);
            _playerState = new PlayerState(_playerConfig.Health, _playerConfig.Armor, _playerConfig.ExperionceMultiplier, _playerConfig.MoneyMultiplier,
                heathlsBar, experinceBar, PlayerMover, PlayerAttacker, PlayerAnimator, PlayerRotater, gameState, playerLevelView, spellMenuOpen);
            _playerUpgradeHandler = new PlayerUpgradeHandler(_weaponPresenter.Weapon, PlayerMover, _playerState, _playerCollectTrigger);
            PlayerDamagable = _playerState;
            PlayerUpgradeHadler = _playerUpgradeHandler;
        }

        public void Initialize()
        {
            PlayerAttacker.Initialize();
            _playerCollectTrigger.Construct(_playerConfig.CollectAreaRadius, _playerState);
            _weaponPresenter.transform.SetParent(_weaponPoint);
            _weaponPresenter.transform.localPosition = Vector3.zero;
            _weaponPresenter.transform.localEulerAngles = new Vector3(90, 0, 0);
        }

        public void Resume()
        {
            PlayerFX.Resume();
            PlayerMover.Resume();
            PlayerRotater.Resume();
            PlayerAnimator.Resume();
            PlayerAttacker.Resume();
        }

        public void Pause()
        {
            PlayerFX.Pause();
            PlayerMover.Pause();
            PlayerRotater.Pause();
            PlayerAnimator.Pause();
            PlayerAttacker.Pause();
        }

        private void Update() => PlayerRotater.Update();
    }
}
