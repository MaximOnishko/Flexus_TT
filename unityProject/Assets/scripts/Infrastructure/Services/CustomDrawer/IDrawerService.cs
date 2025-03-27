using Infrastructure.Services.CustomPhysics;

namespace CodeBase.Infrastructure.Services.CustomDrawer
{
    public interface IDrawerService : IService
    {
        void DrawHit(TrajectoryData.HitData hitData);
    }
}