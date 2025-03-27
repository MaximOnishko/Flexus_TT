using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.CustomDrawer;
using Gameplay.BulletFactory;
using Gameplay.FxPool;
using Infrastructure.Services.CustomPhysics;
using UnityEngine;

namespace Gameplay.Bullet.BulletService
{
    public class BulletService : IBulletService
    {
        private readonly IBulletPool _pool;
        private readonly IBulletMover _mover;
        private readonly IDrawerService _drawer;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IFxPoolService _fxPoolService;

        public BulletService(IBulletPool bulletPool, IFxPoolService fxPool, IDrawerService drawerService,
            ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            
            _fxPoolService = fxPool;
            _pool = bulletPool;
            _drawer = drawerService;
            
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
            _coroutineRunner.StartCoroutine(ShowExplosionFx(view.transform.position));
            _pool.ReturnBullet(view);
        }

        private IEnumerator ShowExplosionFx(Vector3 at)
        {
            var fx = _fxPoolService.GetFx(at);
            yield return new WaitForSeconds(2f);
            _fxPoolService.ReturnFx(fx);
        }
    }
}