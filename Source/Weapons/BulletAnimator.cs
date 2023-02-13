using UnityEngine;

namespace Assets.Source
{
    public class BulletAnimator : IBulletAnimator, IGameObjectsPlayBackHandler
    {
        private readonly ParticleSystem _trail;
        private readonly ParticleSystem _decal;
        private readonly ParticleSystem _missile;
        private readonly ParticleSystem _bulletExplosion;
        private readonly Transform _model;
        private readonly Collider _collider;

        private bool _isGameOnPause = false;

        public float DecalLife => _decal.main.startLifetimeMultiplier;

        public BulletAnimator(Transform model, Collider collider, ParticleSystem trail, ParticleSystem decal, ParticleSystem bulletExplosion, ParticleSystem missile)
        {
            _trail = trail;
            _model = model;
            _collider = collider;
            _decal = decal;
            _bulletExplosion = bulletExplosion;
            _missile = missile;
        }

        public void Resume()
        {
            _isGameOnPause = false;
            SwitchPlayBackParticles(_trail);
            SwitchPlayBackParticles(_missile);
            SwitchPlayBackParticles(_decal);
            SwitchPlayBackParticles(_bulletExplosion);
        }

        public void Pause()
        {
            _isGameOnPause = true;
            SwitchPlayBackParticles(_trail);
            SwitchPlayBackParticles(_missile);
            SwitchPlayBackParticles(_decal);
            SwitchPlayBackParticles(_bulletExplosion);
        }

        public void Enable()
        {
            _model.gameObject.SetActive(true);
            _collider.enabled = true;
            _trail.Play();
        }

        public void Disable()
        {
            _model.gameObject.SetActive(false);
            _collider.enabled = false;
        }

        public void OnCollison(Collider other, bool isEnemy)
        {
            if (_isGameOnPause)
                return;

            Vector3 position = other.ClosestPoint(_decal.transform.position);

            switch (isEnemy)
            {
                case true:
                    SetPositionAndPlay(position, _bulletExplosion);
                    break;
                case false:
                    _decal.transform.rotation = Quaternion.LookRotation(position - _decal.transform.position, Vector3.down);
                    SetPositionAndPlay(position, _decal);
                    break;
            }
        }

        private void SetPositionAndPlay(Vector3 position, ParticleSystem particle)
        {
            particle.transform.position = position;
            particle.Play();
        }

        private void SwitchPlayBackParticles(ParticleSystem particle)
        {
            if (particle.isPaused)
                particle.Play();
            else if (particle.isPlaying)
                particle.Pause();
        }
    }
}
