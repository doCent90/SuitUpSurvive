using UnityEngine;

namespace Assets.Source
{
    public class Money : Collectable
    {
        protected override void GiveCollectable(int enemyLevel, IPlayerCollectables player)
        {
            float moneyValue = Value * enemyLevel;
            player.TakeMoney(moneyValue);
            gameObject.SetActive(false);
        }
    }
}
