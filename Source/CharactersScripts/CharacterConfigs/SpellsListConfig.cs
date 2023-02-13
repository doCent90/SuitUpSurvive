using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    [DefaultExecutionOrder(-5000)]
    [CreateAssetMenu(fileName = "Spell List Config", menuName = "Spell List Config", order = 51)]
    public class SpellsListConfig : ScriptableObject
    {
        [SerializeField] private List<SpellConfig> _defaulsSpells = new();
        [SerializeField] private List<SpellConfig> _specialSpells = new();

        public IReadOnlyList<SpellConfig> DefaultSpells => _defaulsSpells;
        public IReadOnlyList<SpellConfig> SpecialSpells => _specialSpells;
    }
}
