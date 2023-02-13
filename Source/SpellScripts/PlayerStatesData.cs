using System;
using UnityEngine;

namespace Assets.Source
{
    [Serializable]
    public struct PlayerStatesData
    {
        [field: SerializeField] public PlayerStatesVariants PlayerStatesVariants { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}
