using System;
using Infrastructure.Services.CustomPhysics;

namespace Gameplay.Bullet.BulletService
{
    public interface IBulletMover
    {
        public void Move(BulletView view, TrajectoryData trajectoryData, float speed);
        event Action<TrajectoryData.HitData> OnBulletHit;
        event Action<BulletView> OnMoveEnded;
    }
}