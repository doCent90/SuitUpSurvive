using System;

namespace Assets.Source
{
    [Serializable]
    public struct WeaponData
    {
        public int BulletCount;
        public float CooldownBeetwenMissiles;
        public float BulletSpeed;
        public float PushPower;
        public float Cooldown;
        public float Damage;
        public float Range;

        public WeaponData(int defaultCount, float bulletSpeed, float pushPower, float cooldown, float damage, float range, float cooldownBeetwenMissiles)
        {
            BulletCount = defaultCount;
            CooldownBeetwenMissiles = cooldownBeetwenMissiles;
            BulletSpeed = bulletSpeed;
            PushPower = pushPower;
            Cooldown = cooldown;
            Damage = damage;
            Range = range;
        }
    }
}
