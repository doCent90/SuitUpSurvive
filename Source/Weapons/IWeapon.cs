namespace Assets.Source
{
    public interface IWeapon
    {
        void AddedMassiveAttack(float value);
        void DecreaseColldown(float value);
        void DecreaseColldownMissiles(float value);
        void IncreaseBulletsCount(int value);
        void IncreaseBulletSpeed(float value);
        void IncreaseDamage(float value);
        void IncreasePushPower(float value);
        void IncreaseRange(float value);
    }
}
