using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.CustomDrawer;
using Gameplay.BulletFactory;
using Infrastructure.Services.CustomPhysics;

namespace Gameplay.Bullet.BulletService
{
    public class BulletService : IBulletService
    {
        private readonly IBulletPool _pool;
        private readonly IBulletMover _mover;
        private readonly IDrawerService _drawer;

        public BulletService()
        {
            _pool = AllServices.Container.Single<IBulletPool>();
            _drawer = AllServices.Container.Single<IDrawerService>();
            
            _mover = new CoroutineBulletMover();
            
            _mover.OnBulletHit += BulletHit;
            _mover.OnMoveEnded += MoveEnded;
        }

        public void Fire(TrajectoryData trajectoryData, float speed)
        {
            _mover.Move(
                _pool.GetBullet(trajectoryData.Points[0]),
                trajectoryData,
                speed);
        }

        private void BulletHit(TrajectoryData.HitData hitData)
        {
            _drawer.DrawHit(hitData);
        }

        private void MoveEnded(BulletView view)
        {
            _pool.ReturnBullet(view);
        }
    }
}