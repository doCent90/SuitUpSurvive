namespace Assets.Source
{
    public class EnemyState : CharacterState, IEnemyDamageable
    {
        private readonly IEnemyDamageable _enemyDamageable;

        public EnemyState(float healthPoints, float armorPoints, IEnemyDamageable enemyDamageable) : base(healthPoints, armorPoints)
            => _enemyDamageable = enemyDamageable;

        public void TakeDamage(IBulletData bullet)
        {
            DecrasePoints(bullet.Damage);
            _enemyDamageable.TakeDamage(bullet);
        }

        public override void OnDead() => _enemyDamageable.OnDead();
    }
}
