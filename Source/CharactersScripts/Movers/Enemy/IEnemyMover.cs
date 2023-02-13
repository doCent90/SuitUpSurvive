using UnityEngine;

namespace Assets.Source
{
    public interface IEnemyMover
    {
        void OnDead();
        void Stop();
        void Push(float power, Vector3 position);
    }
}
