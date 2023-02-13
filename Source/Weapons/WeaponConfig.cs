using UnityEngine;

namespace Assets.Source
{
    [DefaultExecutionOrder(-5000)]
    [CreateAssetMenu(fileName = "Weapon player Config", menuName = "Weapon player Config", order = 52)]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
    }
}
