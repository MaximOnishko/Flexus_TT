using CodeBase.Infrastructure.Services;
using Gameplay.Bullet;
using UnityEngine;

namespace Gameplay.BulletFactory
{
    public interface IBulletPool : IService
    {
        public BulletView GetBullet(Vector3 trajectoryDataPoint);
        public void ReturnBullet(BulletView bulletView);
        void Init();
    }
}