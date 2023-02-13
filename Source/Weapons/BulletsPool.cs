using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source
{
    public class BulletsPool : IBulletsPool
    {
        private const int Multiplier = 20;
        private const string BulletsContainer = "BulletsContainer";

        private readonly List<BulletPresenter> _bulletsPool = new();
        private readonly BulletPresenter _bullet;

        private GameObject _container;
        private int _count;

        public IReadOnlyList<BulletPresenter> Bullets => _bulletsPool;

        public BulletsPool(BulletPresenter bulletPresenter, int count)
        {
            _count = count;
            _bullet = bulletPresenter;
        }

        public void CreatePool()
        {
            _bulletsPool.Clear();

            if(_container != null)
            {
                Object.Destroy(_container);
                _container = null;
            }

            _container = new GameObject(BulletsContainer);

            for (int count = 0; count < _count * Multiplier; count++)
            {
                BulletPresenter bullet = Object.Instantiate(_bullet);
                bullet.Construct();
                bullet.transform.SetParent(_container.transform);
                bullet.gameObject.SetActive(false);
                _bulletsPool.Add(bullet);
            }
        }

        public void IncreasePool(int increseCount)
        {
            _count = increseCount;
            CreatePool();
        }

        public bool TryGetBullet(out BulletPresenter bullet)
        {
            bullet = _bulletsPool.FirstOrDefault(bullet => bullet.gameObject.activeSelf == false);
            return bullet != null;
        }
    }
}
