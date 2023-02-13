using UnityEngine;

namespace Assets.Source
{
    public interface IBulletAnimator
    {
        float DecalLife { get; }
        void Disable();
        void Enable();
        void OnCollison(Collider other, bool isEnemy);
    }
}
