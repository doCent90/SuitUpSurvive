using System;
using UnityEngine;

namespace Assets.Source
{
    public class PlayerFX : IParticles, IGameObjectsPlayBackHandler
    {
        private readonly ParticleSystem _money;
        private readonly ParticleSystem _trail;
        private readonly ParticleSystem _attackFx;

        public PlayerFX(ParticleSystem money, ParticleSystem trail, ParticleSystem attackFx)
        {
            _money = money;
            _trail = trail;
            _attackFx = attackFx;
        }

        public void OnAttacked() => _attackFx.Play();

        public void Resume()
        {
            SwitchPlayBackParticles(_attackFx);
            SwitchPlayBackParticles(_trail);
            SwitchPlayBackParticles(_money);
        }

        public void Pause()
        {
            SwitchPlayBackParticles(_attackFx);
            SwitchPlayBackParticles(_trail);
            SwitchPlayBackParticles(_money);
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
