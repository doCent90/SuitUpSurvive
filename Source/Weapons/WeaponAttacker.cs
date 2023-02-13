using System;
using UnityEngine;
using System.Collections;
namespace Assets.Source
{
    public class WeaponAttacker : IAttack, IGameObjectsPlayBackHandler
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBulletsPool _bulletsPool;
        private readonly Weapon _weaponMain;
        private readonly Transform _startPoint;
        private readonly Transform _directionPoint;
        private readonly MassiveAttackModule _massiveAttackModule;

        private bool _isGameOnPause = false;
        private bool _isAttacking = false;

        public WeaponAttacker(ICoroutineRunner coroutineRunner, IBulletsPool bulletsPool, Weapon weaponMain, Transform startPoint, Transform directionPoint, MassiveAttackModule massiveAttackModule)
        {
            _weaponMain = weaponMain;
            _startPoint = startPoint;
            _bulletsPool = bulletsPool;
            _directionPoint = directionPoint;
            _coroutineRunner = coroutineRunner;
            _massiveAttackModule = massiveAttackModule;
        }

        private event Action Attacked;

        public void Attack(Action onAction = null)
        {
            Attacked = onAction;
            _isAttacking = true;
        }

        public void Resume() => _isGameOnPause = false;
        public void Pause() => _isGameOnPause = true;
        public void StartAttack() => _coroutineRunner.StartCoroutine(Attacking());
        public void StopAttack() => _isAttacking = false;

        private IEnumerator Attacking()
        {
            var onPause = new WaitWhile(() => _isGameOnPause);
            var waitClick = new WaitUntil(() => _isAttacking);
            var waitBetweenAttack = new WaitForSecondsRealtime(_weaponMain.WeaponData.Cooldown);
            var waitBetweenMissiles = new WaitForSecondsRealtime(_weaponMain.WeaponData.CooldownBeetwenMissiles);

            while (true)
            {
                for (int main = 0; main < _weaponMain.WeaponData.BulletCount; main++)
                {
                    yield return waitClick;
                    yield return onPause;

                    for (int massive = 0; massive < _weaponMain.CountMassiveAttack; massive++)
                    {
                        if (_bulletsPool.TryGetBullet(out BulletPresenter bullet))
                        {
                            Attacked?.Invoke();

                            if (massive == 0)
                                bullet.Bullet.Lauch(_weaponMain.WeaponData, _startPoint.position, _directionPoint.position);
                            else if(_massiveAttackModule.DirectionsMassiveAttack.Count > 0)
                                bullet.Bullet.Lauch(_weaponMain.WeaponData, _startPoint.position, _massiveAttackModule.DirectionsMassiveAttack[massive].position);
                        }
                    }

                    yield return waitBetweenMissiles;
                }

                yield return waitBetweenAttack;
            }
        }
    }
}
