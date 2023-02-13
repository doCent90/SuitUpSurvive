namespace Assets.Source
{

    public abstract class CharacterState : IDead
    {
        protected float MaxHealthPoints;
        protected float HealthPoints;
        protected float ArmorPoints;

        public CharacterState(float healthPoints, float armorPoints)
        {
            HealthPoints = healthPoints;
            MaxHealthPoints = healthPoints;
            ArmorPoints = armorPoints;
        }

        protected void DecrasePoints(float damage)
        {
            damage -= ArmorPoints;

            if (damage - ArmorPoints <= 0)
                return;

            HealthPoints -= damage;

            if (HealthPoints < 0)
            {
                HealthPoints = 0;
                OnDead();
            }
        }

        public abstract void OnDead();
    }
}
