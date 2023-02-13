using UnityEngine;

namespace Assets.Source
{
    public class PlayerRotater : IGameObjectsPlayBackHandler, IPlayerTransition
    {
        private readonly Transform _aim;
        private readonly Transform _model;

        private bool _isGameOnPause = false;
        private bool _isStop = false;

        public PlayerRotater(Transform aim, Transform model)
        {
            _aim = aim;
            _model = model;
        }

        public void Resume() => _isGameOnPause = false;

        public void Pause() => _isGameOnPause = true;

        public void OnDead() => _isStop = true;

        public void OnWin() => _isStop = true;

        public void Update()
        {
            if (_isGameOnPause || _isStop)
                return;

            Rotate();
        }

        private void Rotate()
            => _model.LookAt(new Vector3(_aim.transform.position.x, _model.position.y, _aim.transform.position.z));
    }
}
