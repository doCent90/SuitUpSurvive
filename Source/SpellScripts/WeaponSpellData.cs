using System;
using UnityEngine;

namespace Assets.Source
{
    [Serializable]
    public struct WeaponSpellData
    {
        [field: SerializeField] public AttackSpellVariants AttackSpellVariants { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}
