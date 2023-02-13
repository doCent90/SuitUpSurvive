using UnityEngine;

namespace Assets.Source
{
    public class PlayerAnimator : IAnimator, IInputAxis, IGameObjectsPlayBackHandler, IPlayerTransition
    {
        private const string Win = "Win";
        private const string Strafe = "Strafe";
        private const string Forward = "Forward";
        private const string Attack = "Shooting";
        private const string Dead = "Dead";

        private readonly Camera _mainCamera;
        private readonly PlayerFX _playerFX;
        private readonly Animator _animator;
        private readonly Transform _model;

        private bool _isGameOnPause = false;

        public PlayerAnimator(PlayerFX playerFX, Camera mainCamera, Transform model, Animator animator)
        {
            _mainCamera = mainCamera;
            _playerFX = playerFX;
            _animator = animator;
            _model = model;
        }

        public void Resume()
        {
            _isGameOnPause = false;
            _animator.StopPlayback();
        }

        public void Pause()
        {
            _animator.StartPlayback();
            _isGameOnPause = true;
        }

        public void PlayAttack()
        {
            _playerFX.OnAttacked();
            _animator.SetBool(Attack, true);
        }

        public void StopAttack() => _animator.SetBool(Attack, false);

        public void OnWin() => _animator.SetTrigger(Win);

        public void OnDead() => _animator.SetTrigger(Dead);

        public void OnAxisChanged(float horizontal, float vertical)
        {
            if (_isGameOnPause)
                return;

            Vector3 cameraForward = _mainCamera.transform.forward;
            Vector3 cameraRight = _mainCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            Vector2 direction = new Vector2(horizontal, vertical);

            Vector3 desiredDirection = cameraForward * direction.y + cameraRight * direction.x;

            Vector3 movement = new Vector3(desiredDirection.x, 0f, desiredDirection.z);
            float forw = Vector3.Dot(movement, _model.forward);
            float stra = Vector3.Dot(movement, _model.right);

            _animator.SetFloat(Forward, forw);
            _animator.SetFloat(Strafe, stra);
        }
    }
}
