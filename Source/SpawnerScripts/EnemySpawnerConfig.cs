using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    [DefaultExecutionOrder(-5000)]
    [CreateAssetMenu(fileName = "Enemy Prefab List Config", menuName = "Enemy Prefab List Config", order = 51)]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyPresenterPrefabsList> _enemyWavesPrefabsList;

        public IReadOnlyList<EnemyPresenterPrefabsList> EnemyPrefabsList => _enemyWavesPrefabsList;
    }
}
