namespace Assets.Source
{
    public interface IPlayerStatesUpgrade
    {
        void OnMaxHealthUpgraded(float value);
        void OnArmorUpgraded(float value);
        void OnMoneyMiltiUpgraded(float value);
        void OnExperienceUpGraded(float value);
    }
}
