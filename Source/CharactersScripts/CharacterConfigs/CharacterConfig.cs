using UnityEngine;

namespace Assets.Source
{
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Armor { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}
