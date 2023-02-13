using UnityEngine;

namespace Assets.Source
{
    [DefaultExecutionOrder(-5000)]
    [CreateAssetMenu(fileName = "Player States Config", menuName = "Player Data Config", order = 51)]
    public class PlayerConfig : CharacterConfig
    {
        [field: SerializeField] public float CollectAreaRadius { get; private set; }
        [field: SerializeField] public float ExperionceMultiplier { get; private set; }
        [field: SerializeField] public float MoneyMultiplier { get; private set; }
    }
}
