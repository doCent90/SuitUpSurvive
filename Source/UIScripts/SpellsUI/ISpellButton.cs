namespace Assets.Source
{
    public interface ISpellButton
    {
        void OnButtonSelectDefaultSpellClicked(int spellIndex);
        void OnButtonSelectSpecialSpellClicked(int spellIndex);
    }
}
