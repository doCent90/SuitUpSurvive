using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source
{
    public class CollectableMovement : IGameObjectsPlayBackHandler
    {
        private const float MinDistace = 3f;

        private readonly CollectableAnimator _collectableAnimator;
        private readonly ParticleSystem _particle;
        private readonly Transform _transform;
        private readonly Transform _model;
        private readonly ICoroutineRunner _coroutine;

        private bool _isGameOnPause = false;

        public CollectableMovement(CollectableAnimator collectableAnimator, ParticleSystem particle, Transform transform, Transform model, ICoroutineRunner coroutine)
        {
            _collectableAnimator = collectableAnimator;
            _particle = particle;
            _transform = transform;
            _coroutine = coroutine;
            _model = model;
        }

        public void Resume() => _isGameOnPause = false;

        public void Pause() => _isGameOnPause = true;

        public void OnCollect(Transform playerTransform, Action onAction)
            => _coroutine.StartCoroutine(Moving(playerTransform, onAction));

        private IEnumerator Moving(Transform playerTransform, Action onAction)
        {
            var wait = new WaitForFixedUpdate();
            var onPause = new WaitWhile(() => _isGameOnPause);
            var waitParticle = new WaitWhile(() => _particle.isPlaying);

            float speed = 0;
            float distance = Vector3.SqrMagnitude(playerTransform.position);

            while(distance > MinDistace)
            {
                distance = Vector3.SqrMagnitude(_transform.position - playerTransform.position);
                _transform.position = Vector3.Lerp(_transform.position, playerTransform.position, speed);
                speed += Time.deltaTime;

                yield return wait;
                yield return onPause;
            }

            _collectableAnimator.Play();
            _model.gameObject.SetActive(false);

            yield return waitParticle;
            onAction.Invoke();
        }
    }
}
