using System;

namespace Assets.Source
{
    public class PlayerAttacker : IAttack, IGameObjectsPlayBackHandler, IPlayerTransition
    {
        private readonly Weapon _weapon;
        private readonly IAnimator _playerAnimator;

        private bool _isGameOnPause = false;
        private bool _isStop = false;

        public PlayerAttacker(Weapon defaultWeapon, IAnimator playerAnimator)
        {
            _playerAnimator = playerAnimator;
            _weapon = defaultWeapon;
        }

        public void Initialize() => StartAttack();

        public void Resume() => _isGameOnPause = false;

        public void Pause() => _isGameOnPause = true;

        public void StartAttack() => _weapon.StartAttack();

        public void Attack(Action onAction)
        {
            if (_isGameOnPause || _isStop)
                return;

            _weapon.Attack(() => OnAttacked());
        }

        public void StopAttack()
        {
            _weapon.StopAttack();
            _playerAnimator.StopAttack();
        }

        private void OnAttacked() => _playerAnimator.PlayAttack();

        public void OnDead()
        {
            NewMethod();
        }

        private void NewMethod()
        {
            StopAttack();
            _isStop = true;
        }

        public void OnWin()
        {
            StopAttack();
            _isStop = true;
        }
    }
}
