using System.Collections.Generic;

namespace Assets.Source
{
    public interface IBulletsPool
    {
        void IncreasePool(int increseCount);
        bool TryGetBullet(out BulletPresenter bullet);
    }
}
