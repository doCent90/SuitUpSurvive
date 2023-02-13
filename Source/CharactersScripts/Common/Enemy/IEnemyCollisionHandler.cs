using UnityEngine;

namespace Assets.Source
{
    public interface IEnemyCollisionHandler
    {
        void OnCollisionEnter(Collider other);
        void OnCollisionExit(Collider other);
    }
}
