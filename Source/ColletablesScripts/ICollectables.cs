using UnityEngine;

namespace Assets.Source
{
    public interface ICollectables
    {
        void OnPlayerCollected(IPlayerCollectables player, Transform playerTransform);
    }
}
