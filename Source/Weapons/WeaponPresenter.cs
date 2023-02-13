using UnityEngine;

namespace Assets.Source
{
    public class WeaponPresenter : Presenter, ICoroutineRunner
    {
        [SerializeField] private BulletsPoolPresenter _bulletsPoolPresenter;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _directionPoint;

        public Weapon Weapon { get; private set; }
        public BulletsPoolPresenter BulletsPoolPresenter => _bulletsPoolPresenter;

        public void Construct(WeaponConfig config)
        {
            _bulletsPoolPresenter.Construct(config.WeaponData.BulletCount);
            Weapon = new Weapon(config.WeaponData, _startPoint, _directionPoint, _bulletsPoolPresenter.BulletsPool, this);
        }
    }
}
