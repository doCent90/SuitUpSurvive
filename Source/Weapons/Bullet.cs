using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    public class Bullet : IBullet, IBulletData, IGameObjectsPlayBackHandler
    {
        private readonly Transform _parent;
        private readonly Transform _transform;
        private readonly ICoroutineRunner _coroutine;
        private readonly IBulletAnimator _animator;
        private float _range;
        private bool _isReach = false;
        private bool _isGameOnPause = false;

        public Vector3 Position => _transform.position;
        public float Damage { get; private set; }
        public float PushPower { get; private set; }

        public Bullet(Transform transform, ICoroutineRunner coroutine, IBulletAnimator animator)
        {
            _transform = transform;
            _coroutine = coroutine;
            _animator = animator;
            _parent = transform.parent;
        }

        public void Resume() => _isGameOnPause = false;
        public void Pause() => _isGameOnPause = true;

        public void Lauch(WeaponData weapon, Vector3 start, Vector3 direction)
        {
            _isReach = false;
            Damage = weapon.Damage;
            PushPower = weapon.PushPower;
            _range = weapon.Range;
            Enable();
            Move(weapon, start, direction);
        }

        public void DisableOnCollision()
        {
            if (_isGameOnPause)
                return;

            _isReach = true;
        }

        private void Enable()
        {
            _transform.gameObject.SetActive(true);
            EnableModel();
        }

        private void Disable()
        {
            _transform.SetParent(_parent);
            _transform.gameObject.SetActive(false);
        }

        private void EnableModel() => _animator.Enable();
        private void DisableModel() => _animator.Disable();

        private void Move(WeaponData weapon, Vector3 start, Vector3 direction)
        {
            _coroutine.StartCoroutine(Moving(start, direction));

            IEnumerator Moving(Vector3 start, Vector3 direction)
            {
                float timeForReady = _animator.DecalLife;
                var wait = new WaitForFixedUpdate();
                var onPause = new WaitWhile(() => _isGameOnPause);
                var waitForReady = new WaitForSeconds(timeForReady);
                _transform.SetPositionAndRotation(start, Quaternion.LookRotation(direction - _transform.position, Vector3.down));

                while (_range > 0)
                {
                    yield return onPause;
                    yield return wait;

                    if (_isReach)
                    {
                        _range = 0;
                    }
                    else
                    {
                        _range -= Time.deltaTime;
                        _transform.position = Vector3.MoveTowards(_transform.position, direction, weapon.BulletSpeed * Time.deltaTime);
                    }
                }

                DisableModel();
                yield return waitForReady;
                Disable();
            }
        }
    }
}
