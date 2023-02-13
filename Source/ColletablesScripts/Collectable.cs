using UnityEngine;

namespace Assets.Source
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectable : Presenter, ICollectables, IGameObjectsPlayBackHandler, ICoroutineRunner
    {
        [SerializeField] private Collider _trigger;
        [SerializeField] private Transform _model;
        [SerializeField] private ParticleSystem _particle;

        private CollectableAnimator _collectableAnimator;
        private CollectableMovement _collectableMovement;

        protected float Value = 1;
        protected int EnemyLevel;

        public void Construct(int enemyLevel)
        {
            EnemyLevel = enemyLevel;
            _collectableAnimator = new CollectableAnimator(_particle, transform);
            _collectableMovement = new CollectableMovement(_collectableAnimator, _particle, transform, _model, this);
            _collectableAnimator.Play();
        }

        private void Update() => _collectableAnimator.Rotate();

        public void Resume()
        {
            _collectableAnimator.Resume();
            _collectableMovement.Resume();
        }

        public void Pause()
        {
            _collectableAnimator.Pause();
            _collectableMovement.Pause();
        }

        public void OnPlayerCollected(IPlayerCollectables player, Transform playerTransform)
        {
            _trigger.enabled = false;
            _collectableAnimator.OnCollect(isCollected: true);
            _collectableMovement.OnCollect(playerTransform, () => GiveCollectable(EnemyLevel, player));
        }

        protected abstract void GiveCollectable(int enemyLevel, IPlayerCollectables player);
    }
}
