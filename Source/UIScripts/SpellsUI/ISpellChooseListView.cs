using System.Collections.Generic;

namespace Assets.Source
{
    public interface ISpellChooseListView
    {
        void OpenSpellMenu(IReadOnlyList<Spell> specialSpells, IReadOnlyList<Spell> defaultSpells);
    }
}
