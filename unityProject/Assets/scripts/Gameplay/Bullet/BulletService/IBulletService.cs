using CodeBase.Infrastructure.Services;
using Infrastructure.Services.CustomPhysics;

namespace Gameplay.Bullet.BulletService
{
    public interface IBulletService : IService
    {
        void Fire(TrajectoryData trajectoryData, float power);
    }
}