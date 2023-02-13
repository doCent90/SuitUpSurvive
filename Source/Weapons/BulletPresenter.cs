using UnityEngine;

namespace Assets.Source
{
    public class BulletPresenter : MonoBehaviour, ICoroutineRunner, IGameObjectsPlayBackHandler
    {
        [SerializeField] private Transform _model;
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _trail;
        [SerializeField] private ParticleSystem _decal;
        [SerializeField] private ParticleSystem _missile;
        [SerializeField] private ParticleSystem _bulletExplosion;

        public Bullet Bullet { get; private set; }
        public BulletAnimator BulletAnimator { get; private set; }

        public void Construct()
        {
            BulletAnimator = new BulletAnimator(_model, _collider, _trail, _decal, _bulletExplosion, _missile);
            Bullet = new Bullet(transform, this, BulletAnimator);
        }

        public void Resume()
        {
            Bullet.Resume();
            BulletAnimator.Resume();
        }

        public void Pause()
        {
            Bullet.Pause();
            BulletAnimator.Pause();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Obstacle _))
            {
                OnCollision(other, isEnemy: false);
            }
            else if (other.TryGetComponent(out EnemyPresenter enemy))
            {
                OnCollision(other, isEnemy: true);
                enemy.EnemyDamagable.TakeDamage(Bullet);
            }
        }

        private void OnCollision(Collider other, bool isEnemy)
        {
            BulletAnimator.OnCollison(other, isEnemy);
            Bullet.DisableOnCollision();
        }
    }
}
