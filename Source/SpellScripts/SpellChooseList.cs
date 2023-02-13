using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    public class SpellChooseList : ISpellMenuOpen, ISpellButton
    {
        private const int Count = 3;

        private readonly List<Spell> _specialSpells = new();
        private readonly List<Spell> _defaulSpells = new();
        private readonly List<Spell> _randomFilteredSpecialSpells = new();
        private readonly List<Spell> _randomFilteredDefaultSpells = new();

        private IGameObjectsPlayBackHandler _playBackHandler;
        private ISpellChooseListView _spellChooseListView;
        private IPlayerUpgradeHadler _playerUpgradeHadler;

        public SpellChooseList(IReadOnlyList<SpellConfig> specialSpells, IReadOnlyList<SpellConfig> defaultSpells)
        {
            foreach (SpellConfig spellPresenter in specialSpells)
            {
                spellPresenter.Construct();
                _specialSpells.Add(spellPresenter.Spell);
            }

            foreach (SpellConfig spellPresenter in defaultSpells)
            {
                spellPresenter.Construct();
                _defaulSpells.Add(spellPresenter.Spell);
            }
        }

        public void Initialize(IPlayerUpgradeHadler playerUpgradeHadler, IGameObjectsPlayBackHandler playBackHandler, ISpellChooseListView spellChooseListView)
        {
            _playerUpgradeHadler = playerUpgradeHadler;
            _playBackHandler = playBackHandler;
            _spellChooseListView = spellChooseListView;
        }

        public void OpenSpellMenu()
        {
            IReadOnlyList<Spell> specialSpells = GetRandomSelectedSpell(_specialSpells, _randomFilteredSpecialSpells);
            IReadOnlyList<Spell> defaultSpells = GetRandomSelectedSpell(_defaulSpells, _randomFilteredDefaultSpells);
            _spellChooseListView.OpenSpellMenu(specialSpells, defaultSpells);
            _playBackHandler.Pause();
        }

        public void OnButtonSelectSpecialSpellClicked(int spellIndex)
        {
            _playerUpgradeHadler.OnUdgraded(_randomFilteredSpecialSpells[spellIndex]);
            _randomFilteredSpecialSpells.Clear();
        }

        public void OnButtonSelectDefaultSpellClicked(int spellIndex)
        {
            _playerUpgradeHadler.OnUdgraded(_randomFilteredDefaultSpells[spellIndex]);
            _randomFilteredDefaultSpells.Clear();
            _playBackHandler.Resume();
        }

        private IReadOnlyList<Spell> GetRandomSelectedSpell(List<Spell> targetSpellList, List<Spell> targetFiteredSpellList)
        {
            List<Spell> spells = new();
            spells.AddRange(targetSpellList);

            if (spells.Count < Count)
                return null;

            for (int i = 0; i < Count; i++)
            {
                int randomIndex = Random.Range(0, spells.Count);
                targetFiteredSpellList.Add(spells[randomIndex]);
                spells.RemoveAt(randomIndex);
            }

            return targetFiteredSpellList;
        }
    }
}
