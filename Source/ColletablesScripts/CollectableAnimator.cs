using UnityEngine;

namespace Assets.Source
{
    public class CollectableAnimator : IGameObjectsPlayBackHandler
    {
        private readonly Transform _transform;
        private readonly ParticleSystem _particle;
        private bool _isGameOnPause = false;
        private bool _hasCollected = false;

        public CollectableAnimator(ParticleSystem particle, Transform transform)
        {
            _particle = particle;
            _transform = transform;
            Rotate();
        }

        public void Resume()
        {
            _isGameOnPause = false;

            if (_particle.isPaused)
                _particle.Play();
        }

        public void Pause()
        {
            _isGameOnPause = true;

            if (_particle.isPlaying)
                _particle.Pause();
        }

        public void Play()
        {
            if (_particle.isPlaying)
                _particle.Stop();

            _particle.Play();
        }

        public void OnCollect(bool isCollected) => _hasCollected = isCollected;

        public void Rotate()
        {
            if (_hasCollected && _isGameOnPause)
                return;

            _transform.eulerAngles += new Vector3(0, 1f, 0);
        }
    }
}
