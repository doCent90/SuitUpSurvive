using System;

namespace Assets.Source
{
    public interface IEnemyAnimator
    {
        void OnStarted();
        void OnDead(Action onEvent);
        void Win();
        void OnAttacked();
    }
}
