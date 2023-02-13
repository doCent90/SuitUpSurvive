using UnityEngine;

namespace Assets.Source
{
    public interface IEnemyPresenter
    {
        Transform Transform { get; }
        void Disable();
    }
}
