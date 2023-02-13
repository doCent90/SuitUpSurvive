using System;
using UnityEngine;

namespace Assets.Source
{
    public class InputHandler : MonoBehaviour, IInputHadler
    {
        private const float Step = 0.1f;
        private const string VerticalAxis = "Vertical";
        private const string HorizontalAxis = "Horizontal";

        private IGameObjectsPlayBackHandler _playBackHandler;
        private IInputAxis _playerAnimator;
        private IInputAxis _playermover;
        private IAttack _playerAttaker;
        private float _vertical;
        private float _horizontal;
        private bool _isGameOnPause = false;
        private bool _isAnyMenuOpened = true;

        private void Update() => SendKeyDownEvents();

        public void Initialize(IGameObjectsPlayBackHandler playBackHandler, IInputAxis playermover, IInputAxis playerAnimator, IAttack playerAttaker)
        {
            _playBackHandler = playBackHandler;
            _playerAnimator = playerAnimator;
            _playerAttaker = playerAttaker;
            _playermover = playermover;
        }

        public void UnlockControlKeys() => _isAnyMenuOpened = false;

        public void LockControlKeys() => _isAnyMenuOpened = true;

        private void SendKeyDownEvents()
        {
            if (_isAnyMenuOpened)
                return;

            if (Input.GetMouseButton(0))
                _playerAttaker.Attack();
            else
                _playerAttaker.StopAttack();

            _vertical = Mathf.Lerp(_vertical, Input.GetAxisRaw(VerticalAxis), Step);
            _horizontal = Mathf.Lerp(_horizontal, Input.GetAxisRaw(HorizontalAxis), Step);

            _playerAnimator.OnAxisChanged(_horizontal, _vertical);
            _playermover.OnAxisChanged(_horizontal, _vertical);

            if (Input.GetKeyDown(KeyCode.Space) && _isGameOnPause == false)
            {
                _playBackHandler.Pause();
                _isGameOnPause = true;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && _isGameOnPause)
            {
                _playBackHandler.Resume();
                _isGameOnPause = false;
            }
        }
    }
}
