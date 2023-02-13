using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    [DefaultExecutionOrder(-5000)]
    [CreateAssetMenu(fileName = "Spell Config", menuName = "Spell Config", order = 51)]
    public class SpellConfig : ScriptableObject
    {
        [Header("View")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [Header("Upgrade Values")]
        [SerializeField] private List<PlayerStatesData> _playerStatsData;
        [SerializeField] private WeaponSpellData _attackSpellData;
        [SerializeField] private DefenceSpellData _defenceSpellData;

        public Spell Spell { get; private set; }

        public void Construct()
        {
            var data = new SpellData(_attackSpellData, _defenceSpellData, _playerStatsData);
            Spell = new Spell(data, _name, _description, _icon);
        }
    }
}
