using UnityEngine;

namespace Assets.Source
{
    public class BulletsPoolPresenter : MonoBehaviour
    {
        [SerializeField] private BulletPresenter _bulletPresenterPrefab;

        private BulletsPool _bulletsPoolCreator;

        public BulletsPool BulletsPool { get; private set; }

        public void Construct(int defaultCount)
        {
            _bulletsPoolCreator = new BulletsPool(_bulletPresenterPrefab, defaultCount);
            _bulletsPoolCreator.CreatePool();
            BulletsPool = _bulletsPoolCreator;
        }
    }
}
