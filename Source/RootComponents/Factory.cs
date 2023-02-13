using UnityEngine;

namespace Assets.Source
{
    public class Factory : IFactory
    {
        public Presenter Create(Presenter presenter, Vector3 position, Transform parent)
        {
            Presenter createdObject = Object.Instantiate(presenter, position, Quaternion.identity, parent);
            return createdObject;
        }
    }
}
