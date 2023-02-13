using UnityEngine;

namespace Assets.Source
{
    [DefaultExecutionOrder(-5000)]
    [CreateAssetMenu(fileName = "Enemy States Config", menuName = "Enemy Data Config", order = 52)]
    public class EnemyConfig : CharacterConfig
    {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float PushPowerResist { get; private set; }
    }
}
