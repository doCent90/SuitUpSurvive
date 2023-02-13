using System.Collections.Generic;

namespace Assets.Source
{
    public struct SpellData
    {
        public IReadOnlyList<PlayerStatesData> PlayerStatsDataList;
        public WeaponSpellData AttackSpellVariants;
        public DefenceSpellData DefenceSpellVariants;

        public SpellData(WeaponSpellData attackSpellVariants, DefenceSpellData defenceSpellVariants, List<PlayerStatesData> playerStatsData)
        {
            AttackSpellVariants = attackSpellVariants;
            DefenceSpellVariants = defenceSpellVariants;
            PlayerStatsDataList = playerStatsData;
        }
    }
}
