using UnityEngine;
using DG.Tweening;
using System;

namespace Assets.Source
{
    public class EnemyAnimator : IEnemyAnimator, IGameObjectsPlayBackHandler
    {
        private const string TextureImpact = "_TextureImpact";
        private const string OutLineWidth = "_OutlineWidth";
        private const string Run = nameof(Run);
        private const string Die = nameof(Die);
        private const string Idle = nameof(Idle);
        private const float BlinkTime = 0.07f;
        private const float Duration = 0.2f;
        private const float DieDuration = 2f;
        private const float Offset = 1.58f;
        private const float DieOffset = -1f;
        private const float Width = 3f;

        private readonly SkinnedMeshRenderer _renderer;
        private readonly ParticleSystem _explosion;
        private readonly ParticleSystem _hole;
        private readonly Animator _animator;
        private readonly Transform _model;

        private Sequence _sequence;
        private Tween _start;
        private Tween _die;

        public float StartDelay { get; private set; } = 1.9156f;

        public EnemyAnimator(Animator animator, Transform model, SkinnedMeshRenderer renderer, ParticleSystem hole, ParticleSystem explosion)
        {
            _renderer = renderer;
            _hole = hole;
            _explosion = explosion;
            _animator = animator;
            _model = model;
        }

        public void Resume() => SetResume();

        public void Pause() => SetPause();

        public void OnStarted()
        {
            _model.position = new Vector3(_model.position.x, _model.position.y - Offset, _model.position.z);
            _start = _model.DOMoveY(0, Duration).SetDelay(StartDelay).SetEase(Ease.Linear).OnComplete(() => _start = null);
        }

        public void Win() => _animator.SetTrigger(Idle);

        public void OnDead(Action onAction)
        {
            _animator.SetTrigger(Die);
            _die = _model.DOMoveY(DieOffset, DieDuration).SetDelay(DieDuration / 2)
                .OnComplete(() =>
                {
                    _die = null;
                    onAction();
                });
        }

        public void OnAttacked()
        {
            if(_sequence != null)
                _sequence.Complete();

            _sequence = DOTween.Sequence();
            _sequence.Append(_renderer.material.DOFloat(0.5f, TextureImpact, BlinkTime));
            _sequence.Join(_renderer.material.DOFloat(Width, OutLineWidth, BlinkTime));
            _sequence.Append(_renderer.material.DOFloat(1, TextureImpact, BlinkTime));
            _sequence.Join(_renderer.material.DOFloat(0, OutLineWidth, BlinkTime));
            _sequence.OnComplete(() => _sequence = null);
        }

        private void SetResume()
        {
            SetPlayAnimation(_animator, _sequence);
            SwitchPlayBackParticles(_hole);
            SwitchPlayBackParticles(_explosion);
            SetPlayTween(_die);
            SetPlayTween(_start);
        }

        private void SetPause()
        {
            SetPauseAnimation(_animator, _sequence);
            SwitchPlayBackParticles(_hole);
            SwitchPlayBackParticles(_explosion);
            SetPauseTween(_die);
            SetPauseTween(_start);
        }

        private void SetPlayAnimation(Animator animator, Sequence sequence)
        {
            if(sequence != null)
                sequence.Play();

            animator.StopPlayback();
        }

        private void SetPauseAnimation(Animator animator, Sequence sequence)
        {
            if(sequence != null)
                sequence.Pause();

            animator.StartPlayback();
        }

        private void SetPlayTween(Tween tween)
        {
            if (tween != null)
                tween.Play();
        }

        private void SetPauseTween(Tween tween)
        {
            if (tween != null)
                tween.Pause();
        }

        private void SwitchPlayBackParticles(ParticleSystem particle)
        {
            if(particle.isPaused)
                particle.Play();
            else if(particle.isPlaying)
                particle.Pause();
        }
    }
}
