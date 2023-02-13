using UnityEngine;

namespace Assets.Source
{
    public class Enemy : IEnemyDamageable, IEnemy
    {
        private readonly Collider _enemyCollider;
        private readonly Transform _currecntPosition;
        private readonly IEnemyMover _mover;
        private readonly IEnemyDeadState _spawner;
        private readonly IEnemyAnimator _animator;
        private readonly IEnemyPresenter _presenter;
        private readonly ICollectablesSpawner _collectablesSpawner;
        private readonly IGameObjectsPlayBackHandler _playBackHandler;
        private readonly int _level;

        public float Damage { get; private set; }

        public Enemy(float damage, int level, IEnemyPresenter presenter, IEnemyDeadState spawner, IEnemyMover mover, IEnemyAnimator animator,
            Collider collider, Transform currecntPosition, ICollectablesSpawner collectablesSpawner, IGameObjectsPlayBackHandler playBackHandler)
        {
            Damage = damage;
            _level = level;
            _mover = mover;
            _spawner = spawner;
            _animator = animator;
            _presenter = presenter;
            _enemyCollider = collider;
            _currecntPosition = currecntPosition;
            _collectablesSpawner = collectablesSpawner;
            _playBackHandler = playBackHandler;
        }

        public void OnDead()
        {
            _collectablesSpawner.Spawn(_level, _currecntPosition.position);
            _enemyCollider.enabled = false;
            _mover.OnDead();
            _animator.OnDead(() =>
            {
                _spawner.AddDeadEnemy(_presenter, _playBackHandler);
                _presenter.Disable();
            });
        }

        public void TakeDamage(IBulletData bullet)
        {
            _mover.Push(bullet.PushPower, bullet.Position);
            _animator.OnAttacked();
        }

        public void OnPlayerDead()
        {
            _mover.Stop();
            _animator.Win();
        }
    }
}
