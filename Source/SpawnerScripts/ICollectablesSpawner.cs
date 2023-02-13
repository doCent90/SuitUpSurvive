using UnityEngine;

namespace Assets.Source
{
    public interface ICollectablesSpawner
    {
        void Spawn(int enemyLevel, Vector3 position);
    }
}
