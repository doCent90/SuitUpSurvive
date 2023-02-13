using UnityEngine;

namespace Assets.Source
{
    public class CollectablesSpawnerPresenter : Presenter
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Money _moneyTemplate;
        [SerializeField] private Experience _experienceTemplate;

        public CollectablesSpawner Spawner { get; private set; }

        public void Construct() => Spawner = new CollectablesSpawner(_moneyTemplate, _experienceTemplate, _container);
    }
}
