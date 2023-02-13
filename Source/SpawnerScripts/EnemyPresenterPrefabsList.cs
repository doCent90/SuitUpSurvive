using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    [Serializable]
    public class EnemyPresenterPrefabsList
    {
        [SerializeField] private EnemyPresenter[] _enemyPrefabs;

        public IReadOnlyList<EnemyPresenter> EnemyPrefabs => _enemyPrefabs;
    }
}
