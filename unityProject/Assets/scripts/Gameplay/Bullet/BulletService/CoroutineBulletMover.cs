using System;
using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using Infrastructure.Services.CustomPhysics;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Bullet.BulletService
{
    public class CoroutineBulletMover : IBulletMover
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public event Action<TrajectoryData.HitData> OnBulletHit;
        public event Action<BulletView> OnMoveEnded;

        public CoroutineBulletMover()
        {
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();
        }

        public void Move(BulletView view, TrajectoryData trajectoryData, float speed)
        {
            _coroutineRunner.StartCoroutine(MoveCoroutine(view, trajectoryData, speed));
        }

        private IEnumerator MoveCoroutine(BulletView view, TrajectoryData trajectoryData, float speed)
        {
            if (trajectoryData.Points.Length == 0)
                yield break;

            int hitCounter = 0;

            for (var i = 0; i < trajectoryData.Points.Length; i++)
            {
                if (i == trajectoryData.LastHitIndex)
                    break;

                while (Vector3.Distance(view.transform.position, trajectoryData.Points[i]) > 0.01f)
                {
                    view.transform.position =
                        Vector3.MoveTowards(view.transform.position, trajectoryData.Points[i], speed * 0.01f);
                    yield return new WaitForSeconds(0.01f);
                }

                if (trajectoryData.HitDatas[hitCounter].TrajectoryIndex == i)
                {
                    if (trajectoryData.HitDatas[hitCounter].Collider != null)
                        OnBulletHit?.Invoke(trajectoryData.HitDatas[hitCounter]);
                    
                    hitCounter++;
                }
            }

            OnMoveEnded?.Invoke(view);
            view.gameObject.SetActive(false);
        }
    }
}