using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Source
{
    public class PlayerMover : IInputAxis, IGameObjectsPlayBackHandler, IPlayerMover, IPlayerTransition
    {
        private readonly NavMeshAgent _agent;
        private bool _isGameOnPause = false;
        private bool _isStop = false;

        public event Action<Vector2> Runnig;

        public PlayerMover(NavMeshAgent agent, float speed)
        {
            _agent = agent;
            _agent.speed = speed;
        }

        public void OnUpgraded(float value) => _agent.speed += value;

        public void Resume()
        {
            _isGameOnPause = false;

            if (_agent.enabled == false)
                _agent.enabled = true;
        }

        public void Pause()
        {
            _isGameOnPause = true;

            if (_agent.enabled)
                _agent.enabled = false;
        }

        public void OnAxisChanged(float x, float z)
        {
            Vector3 direction = new Vector3(x, 0, z) * _agent.speed;
            Move(direction);
        }

        private void Move(Vector3 direction)
        {
            if (_agent.enabled == false || _isGameOnPause || _isStop)
                return;

            _agent.Move(direction);
            Runnig?.Invoke(direction);
        }

        public void OnDead() => Stop();

        public void OnWin() => Stop();

        private void Stop()
        {
            _isStop = true;
            _agent.enabled = false;
        }
    }
}
