using UnityEngine;

namespace Assets.Source
{
    public class AimInWorldSpaceOrientation : MonoBehaviour
    {
        [SerializeField] private Transform _aim;

        private Plane _surfacePlane = new Plane();
        private PlayerPresenter _player;
        private Camera _camera;

        public Transform Aim => _aim;

        private void Update() => _aim.transform.position = GetAimTargetPos();

        public void Initialize(PlayerPresenter player, Camera camera)
        {
            _player = player;
            _camera = camera;
        }

        private Vector3 GetAimTargetPos()
        {
            _surfacePlane.SetNormalAndPosition(Vector3.up, _player.transform.position);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_surfacePlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                return hitPoint;
            }

            return new Vector3(-5000, -5000, -5000);
        }
    }
}
