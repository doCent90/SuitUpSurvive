namespace Assets.Source
{
    public interface IEnemyDamageable : IDead
    {
        void TakeDamage(IBulletData bullet);
    }
}
