using UnityEngine;

namespace Assets.Source
{
    public interface IFactory
    {
        Presenter Create(Presenter presenter, Vector3 position, Transform parent);
    }
}
