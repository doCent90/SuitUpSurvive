using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source
{
    public class EnemyAttacker : IAttack, IGameObjectsPlayBackHandler
    {
        private readonly IPlayerDamagable _playerState;
        private readonly ICoroutineRunner _coroutine;
        private readonly float _attackSpeed;
        private readonly float _damage;

        private bool _isCollided;
        private bool _isGameOnPause = false;

        public EnemyAttacker(IPlayerDamagable playerState, ICoroutineRunner coroutine, float attackSpeed, float damage)
        {
            _playerState = playerState;
            _coroutine = coroutine;
            _attackSpeed = attackSpeed;
            _damage = damage;
        }

        public void Resume() => _isGameOnPause = false;

        public void Pause() => _isGameOnPause = true;

        public void StartAttack()
        {
            _isCollided = true;
            _coroutine.StartCoroutine(Attacking());

            IEnumerator Attacking()
            {
                var wait = new WaitForSecondsRealtime(_attackSpeed);
                var onPause = new WaitWhile(() => _isGameOnPause);

                while (_isCollided)
                {
                    yield return onPause;
                    Attack();
                    yield return wait;
                }
            }
        }

        public void Attack(Action onAction = null) => _playerState.TakeDamage(_damage);

        public void StopAttack() => _isCollided = false;
    }
}
