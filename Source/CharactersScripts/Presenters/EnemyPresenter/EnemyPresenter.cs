using UnityEngine;

namespace Assets.Source
{
    public class EnemyPresenter : CharacterPresenter, IEnemyPresenter, ICoroutineRunner, IGameObjectsPlayBackHandler
    {
        [SerializeField] private SkinnedMeshRenderer _meshRendererModel;
        [SerializeField] private Collider _enemyCollider;
        [SerializeField] private Transform _pushPoint;
        [SerializeField] private EnemyConfig _enemyConfig;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _hole;
        [SerializeField] private ParticleSystem _explosion;

        private EnemyState _state;
        private Enemy _enemy;

        private IEnemyCollisionHandler _enemyCollision;

        public Transform Transform => transform;
        public EnemyMover EnemyMover { get; private set; }
        public EnemyAttacker EnemyAttacker { get; private set; }
        public EnemyAnimator EnemyAnimator { get; private set; }
        public IEnemy Enemy { get; private set; }
        public IEnemyDamageable EnemyDamagable { get; private set; }
        public IGameObjectsPlayBackHandler EnemyCollision { get; private set; }

        public void Construct(IPlayerPresenter player, IEnemyDeadState spawner, ICollectablesSpawner collectablesSpawner)
        {
            EnemyAnimator = new EnemyAnimator(Animator, Model, _meshRendererModel, _hole, _explosion);
            EnemyMover    = new EnemyMover(player.PlayerTransform, Agent, _pushPoint, _enemyConfig.Speed, EnemyAnimator.StartDelay, _enemyConfig.PushPowerResist);
            EnemyAttacker = new EnemyAttacker(player.PlayerDamagable, this, _enemyConfig.AttackSpeed, _enemyConfig.Damage);
            _enemy        = new Enemy(_enemyConfig.Damage, _enemyConfig.Level, this, spawner, EnemyMover, EnemyAnimator, _enemyCollider, transform, collectablesSpawner, this);
            _state        = new EnemyState(_enemyConfig.Health, _enemyConfig.Armor, _enemy);
            EmenyCollisionHadler collisionHadler = new EmenyCollisionHadler(EnemyAttacker);

            Enemy = _enemy;
            EnemyDamagable = _state;
            _enemyCollision = collisionHadler;
            EnemyCollision = collisionHadler;

            EnemyAnimator.OnStarted();
        }

        public void Resume()
        {
            EnemyMover.Resume();
            EnemyAnimator.Resume();
            EnemyAttacker.Resume();
            EnemyCollision.Resume();
        }

        public void Pause()
        {
            EnemyMover.Pause();
            EnemyAnimator.Pause();
            EnemyAttacker.Pause();
            EnemyCollision.Pause();
        }

        public void Disable() => gameObject.SetActive(false);

        private void Update() => EnemyMover.Move();

        private void OnTriggerEnter(Collider other) => _enemyCollision.OnCollisionEnter(other);

        private void OnTriggerExit(Collider other) => _enemyCollision.OnCollisionExit(other);
    }
}
