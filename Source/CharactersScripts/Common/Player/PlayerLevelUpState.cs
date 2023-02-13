namespace Assets.Source
{
    public class PlayerLevelUpState : IPlayerLevelUpState
    {
        private readonly ISpellMenuOpen _spellMenuOpen;
        private readonly IPlayerLevelView _playerLevelView;
        private float _multiplierExp;
        private int _level = 1;

        public float ExperiencePoints { get; private set; }
        public float MaxExperiencePoints { get; private set; }  = 10;

        public PlayerLevelUpState(float multiplierExp, IPlayerLevelView playerLevelView, ISpellMenuOpen spellMenuOpen)
        {
            _multiplierExp = multiplierExp;
            _playerLevelView = playerLevelView;
            _spellMenuOpen = spellMenuOpen;
            _playerLevelView.OnLevelChanged(_level);
        }

        public void OnExperienceUpGraded(float value) => _multiplierExp += value;

        public void TakeExperions(float experionsValue)
        {
            ExperiencePoints += experionsValue * _multiplierExp;

            if(ExperiencePoints >= MaxExperiencePoints)
            {
                _level++;
                ExperiencePoints = 0;
                MaxExperiencePoints *= 2;
                _playerLevelView.OnLevelChanged(_level);
                _spellMenuOpen.OpenSpellMenu();
            }
        }
    }
}
