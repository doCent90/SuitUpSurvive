using UnityEngine;

namespace Assets.Source
{
    public class Experience : Collectable
    {
        protected override void GiveCollectable(int enemyLevel, IPlayerCollectables player)
        {
            float experionsValue = Value * enemyLevel;
            player.TakeExperions(experionsValue);
            gameObject.SetActive(false);
        }
    }
}
