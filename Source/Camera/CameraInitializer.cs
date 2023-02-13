using Cinemachine;
using UnityEngine;

namespace Assets.Source
{
    public class CameraInitializer : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        private Transform _playerTransform;

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _camera.Follow = _playerTransform;
            _camera.LookAt = _playerTransform;
        }
    }
}
