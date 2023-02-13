using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace Assets.Source
{
    public class EnemyMover : IEnemyMover, IGameObjectsPlayBackHandler
    {
        private const float PushDuration = 0.5f;
        private const int DefaultPushPower = 1;
        private const int PushPowerDevide = 10;

        private readonly Transform _player;
        private readonly NavMeshAgent _agent;
        private readonly Transform _pushPoint;
        private readonly float _pushPowerResist;
        private readonly float _delay;

        private Tween _pushTween;
        private Vector3 _pushPowerPointDefault;
        private bool _isGameOnPause = false;
        private float _spentTime = 0;
        private bool _isDead = false;

        public EnemyMover(Transform player, NavMeshAgent agent, Transform pushPoint, float speed, float delay, float pushPowerResist)
        {
            _player = player;
            _agent = agent;
            _delay = delay;
            _pushPoint = pushPoint;
            _agent.speed = speed;
            _pushPowerResist = pushPowerResist;
            _pushPowerPointDefault = _pushPoint.localPosition;
        }

        public void Resume()
        {
            _isGameOnPause = false;

            if(_agent.enabled == false && _isDead == false)
                _agent.enabled = true;

            if (_pushTween != null)
                _pushTween.Play();
        }

        public void Pause()
        {
            _isGameOnPause = true;

            if(_agent.enabled)
                _agent.enabled = false;

            if (_pushTween != null)
                _pushTween.Pause();
        }

        public void Move()
        {
            if (CanMove())
                _agent.SetDestination(_player.position);
        }

        public void OnDead()
        {
            _isDead = true;
            _agent.enabled = false;
        }

        public void Push(float power, Vector3 position)
        {
            float offset = (power - _pushPowerResist) / PushPowerDevide;
            if (power < DefaultPushPower || offset < 0 || _agent.enabled == false)
                return;

            _agent.enabled = false;
            _pushPoint.localPosition = new Vector3(0, 0, _pushPowerPointDefault.z - offset);
            _pushTween = _agent.transform.DOMove(_pushPoint.position, PushDuration).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _agent.enabled = true;
                _pushTween = null;
            });
        }

        public void Stop() => _agent.enabled = false;

        private bool CanMove()
        {
            if (_isGameOnPause || _agent.enabled == false || _spentTime < _delay || _isDead)
            {
                if(_isGameOnPause == false)
                    _spentTime += Time.deltaTime;

                return false;
            }

            return true;
        }
    }
}
