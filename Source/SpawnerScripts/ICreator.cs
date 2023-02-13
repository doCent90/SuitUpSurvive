using System.Collections.Generic;

namespace Assets.Source
{
    public interface IEnemyCreator : IEnemySpawner
    {
        void StartCreating();
    }
}
