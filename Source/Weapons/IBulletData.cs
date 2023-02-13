using UnityEngine;

namespace Assets.Source
{
    public interface IBulletData
    {
        Vector3 Position { get; }
        float Damage { get; }
        float PushPower { get; }
    }
}
