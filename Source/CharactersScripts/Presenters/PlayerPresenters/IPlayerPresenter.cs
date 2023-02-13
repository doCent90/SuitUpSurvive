using UnityEngine;

namespace Assets.Source
{
    public interface IPlayerPresenter
    {
        Transform PlayerTransform { get; }
        IPlayerDamagable PlayerDamagable { get; }
    }
}
