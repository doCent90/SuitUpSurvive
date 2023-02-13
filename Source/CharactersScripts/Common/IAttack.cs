using System;

namespace Assets.Source
{
    public interface IAttack
    {
        void StartAttack();
        void Attack(Action onAction = null);
        void StopAttack();
    }
}
