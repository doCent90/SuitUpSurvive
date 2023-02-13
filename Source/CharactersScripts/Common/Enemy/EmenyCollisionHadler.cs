using UnityEngine;

namespace Assets.Source
{
    public class EmenyCollisionHadler : IEnemyCollisionHandler, IGameObjectsPlayBackHandler
    {
        private readonly IAttack _enemyAttack;

        private bool _isGameOnPause = false;

        public EmenyCollisionHadler(IAttack attack) => _enemyAttack = attack;

        public void Resume() => _isGameOnPause = false;

        public void Pause() => _isGameOnPause = true;

        public void OnCollisionEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerPresenter _) && _isGameOnPause == false)
                _enemyAttack.StartAttack();
        }

        public void OnCollisionExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerPresenter _))
                _enemyAttack.StopAttack();
        }
    }
}
