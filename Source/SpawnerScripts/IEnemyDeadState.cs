using UnityEngine;

namespace Assets.Source
{
    public interface IEnemyDeadState
    {
        void AddDeadEnemy(IEnemyPresenter enemy, IGameObjectsPlayBackHandler playBackHandler);
    }
}
