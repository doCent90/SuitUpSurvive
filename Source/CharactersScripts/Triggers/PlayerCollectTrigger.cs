using UnityEngine;

namespace Assets.Source
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCollectTrigger : MonoBehaviour, IPlayerCollectTrigger
    {
        [SerializeField] private SphereCollider _collider;

        private IPlayerCollectables _playerState;
        private float _radius;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Collectable collectable))
                collectable.OnPlayerCollected(_playerState, transform);
        }

        public void Construct(float radius, IPlayerCollectables player)
        {
            _radius = radius;
            _playerState = player;
            _collider.radius = _radius;
        }

        public void IcreaseRadius(float value) => _collider.radius += value;
    }
}
