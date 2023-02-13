using UnityEngine;

namespace Assets.Source
{
    public class Spell
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }
        public SpellData SpellData { get; private set; }

        public Spell(SpellData spellData, string name, string description, Sprite icon)
        {
            SpellData = spellData;
            Name = name;
            Description = description;
            Icon = icon;
        }
    }
}
