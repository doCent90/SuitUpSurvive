using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source
{
    public class CollectablesSpawner : ICollectablesSpawner
    {
        private readonly Transform _container;
        private readonly List<Collectable> _collectablesVariant = new();
        private readonly List<Collectable> _spawnedCollectables = new();

        public IReadOnlyList<Collectable> Collectables => _spawnedCollectables;

        public CollectablesSpawner(Money moneyTamplate, Experience experienceTamplate, Transform container)
        {
            _container = container;
            _collectablesVariant.Add(moneyTamplate);
            _collectablesVariant.Add(experienceTamplate);
            _collectablesVariant.Add(experienceTamplate);
            _collectablesVariant.Add(experienceTamplate);
            _collectablesVariant.Add(experienceTamplate);
        }

        public void Spawn(int enemyLevel, Vector3 position) => Create(enemyLevel, position);

        private void Create(int enemyLevel, Vector3 position)
        {
            Collectable collectable = _collectablesVariant[Random.Range(0, _collectablesVariant.Count)];
            collectable = Object.Instantiate(collectable, position, Quaternion.identity, _container);
            _spawnedCollectables.Add(collectable);
            collectable.Construct(enemyLevel);
        }
    }
}
