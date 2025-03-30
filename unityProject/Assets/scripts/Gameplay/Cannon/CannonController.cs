using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Camera;
using CodeBase.Infrastructure.Services.UI;
using CodeBase.StaticData;
using Gameplay.Bullet.BulletService;
using Infrastructure.Services.CustomPhysics;
using UnityEngine;

namespace Cannon
{
    public class CannonController
    {
        private readonly IInputService _inputService;
        private readonly ICustomPhysicsService _customPhysics;
        private readonly IUIService _uiService;
        private readonly IBulletService _bulletService;
        private readonly ICameraService _cameraService;
        private readonly ICoroutineRunner _coroutineRunner;

        private CannonView _view;
        private CannonData _data;

        public CannonController()
        {
            _cameraService = AllServices.Container.Single<ICameraService>();
            _uiService = AllServices.Container.Single<IUIService>();
            _inputService = AllServices.Container.Single<IInputService>();
            _customPhysics = AllServices.Container.Single<ICustomPhysicsService>();
            _bulletService = AllServices.Container.Single<IBulletService>();
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();

            Subscribe();
        }

        public void Init(CannonView view, CannonStaticData staticData)
        {
            _view = view;
            _data = new CannonData(staticData.BasePower, staticData.BulletStaticData.PowerToSpeedMultiplier);
            
            _coroutineRunner.StartCoroutine(UpdateTrajectory());
        }

        private void Subscribe()
        {
            _inputService.OnClickFire += Fire;
            _inputService.OnRotate += RotateCannon;
            _uiService.OnPowerChanged += UpdatePower;
        }

        private void Fire()
        {
            _cameraService.ShakeCamera();
            _view.Animator.PlayAttack();
            
            _bulletService.Fire(
                _customPhysics.GetTrajectoryData(_view.SpawnBulletPos.position, _view.SpawnBulletPos.forward,
                    _data.Power), _data.BulletSpeed);
        }

        private void RotateCannon(Vector2 pointPos)
        {
            _view.RotateTo(pointPos);
        }

        private void UpdatePower(float sliderVal)
        {
            _data.UpdatePower(sliderVal);
        }

        // private void UpdateTrajectory()
        // {
        //     _view.TrajectoryView.UpdateTrajectory(_customPhysics.GetTrajectoryData(_view.SpawnBulletPos.position,
        //         _view.SpawnBulletPos.forward, _data.Power));
        // }

        private IEnumerator UpdateTrajectory()
        {
            while (true)
            {
                _view.TrajectoryView.UpdateTrajectory(_customPhysics.GetTrajectoryData(_view.SpawnBulletPos.position,
                    _view.SpawnBulletPos.forward, _data.Power));
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}