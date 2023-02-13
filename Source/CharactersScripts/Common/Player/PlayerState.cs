namespace Assets.Source
{
    public class PlayerState : CharacterState, IPlayerDamagable, IPlayerStatesUpgrade, IPlayerTransition, IPlayerCollectables
    {
        private readonly IBar _heatlsBar;
        private readonly IBar _experienceBar;
        private readonly IPlayerGameState _gameState;
        private readonly IPlayerTransition _playerMover;
        private readonly IPlayerTransition _playerRotator;
        private readonly IPlayerTransition _playerAttacker;
        private readonly IPlayerTransition _playerAnimator;
        private readonly PlayerLevelUpState _playerLevelUpState;

        private float _multiplierMoney;
        private float _money;

        public PlayerState(float healthPoints, float armorPoints, float multiplierExp, float multiplierMoney, IBar heatlsBar, IBar experienceBar, IPlayerTransition playerMover, IPlayerTransition playerAttacker,
            IPlayerTransition playerAnimator, IPlayerTransition playerRotator, IPlayerGameState gameState, IPlayerLevelView playerLevelView, ISpellMenuOpen spellMenuOpen) : base(healthPoints, armorPoints)
        {
            _playerLevelUpState = new PlayerLevelUpState(multiplierExp, playerLevelView, spellMenuOpen);
            _heatlsBar = heatlsBar;
            _playerMover = playerMover;
            _experienceBar = experienceBar;
            _multiplierMoney = multiplierMoney;
            _playerAttacker = playerAttacker;
            _playerAnimator = playerAnimator;
            _playerRotator = playerRotator;
            _gameState = gameState;

            _heatlsBar.OnValueChanged(HealthPoints, MaxHealthPoints);
            _experienceBar.OnValueChanged(_playerLevelUpState.ExperiencePoints, _playerLevelUpState.MaxExperiencePoints);
        }

        public void TakeDamage(float damage)
        {
            DecrasePoints(damage);
            _heatlsBar.OnValueChanged(HealthPoints, MaxHealthPoints);
        }

        public void TakeExperions(float experionsValue)
        {
            _playerLevelUpState.TakeExperions(experionsValue);
            _experienceBar.OnValueChanged(_playerLevelUpState.ExperiencePoints, _playerLevelUpState.MaxExperiencePoints);
        }

        public void TakeMoney(float moneyValue) => _money += moneyValue * _multiplierMoney;
        public void OnExperienceUpGraded(float value) => _playerLevelUpState.OnExperienceUpGraded(value);
        public void OnArmorUpgraded(float value) => ArmorPoints += value;

        public void OnMaxHealthUpgraded(float value)
        {
            MaxHealthPoints += value;
            _heatlsBar.OnValueChanged(HealthPoints, MaxHealthPoints);
        }

        public void OnMoneyMiltiUpgraded(float value) => _multiplierMoney += value;

        public override void OnDead()
        {
            _playerMover.OnDead();
            _playerAttacker.OnDead();
            _playerAnimator.OnDead();
            _playerRotator.OnDead();
            _gameState.OnPlayerDead();
        }

        public void OnWin()
        {
            _playerMover.OnWin();
            _playerAttacker.OnWin();
            _playerAnimator.OnWin();
            _playerRotator.OnWin();
        }
    }
}
