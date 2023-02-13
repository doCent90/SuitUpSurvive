using System;
using UnityEngine;

namespace Assets.Source
{
    [Serializable]
    public struct DefenceSpellData
    {
        [field: SerializeField] public DefenceSpellVariants DefenceSpellVariants { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}
