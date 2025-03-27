using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
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

        private CannonView _view;
        private CannonData _data;

        public CannonController()
        {
            _uiService = AllServices.Container.Single<IUIService>();
            _inputService = AllServices.Container.Single<IInputService>();
            _customPhysics = AllServices.Container.Single<ICustomPhysicsService>();
            _bulletService = AllServices.Container.Single<IBulletService>();

            Subscribe();
        }

        public void Init(CannonView view, CannonStaticData staticData)
        {
            _view = view;
            _data = new CannonData(staticData.BasePower);
        }

        private void Subscribe()
        {
            _inputService.OnClickFire += Fire;
            _inputService.OnRotate += RotateCannon;
            _uiService.OnPowerChanged += UpdatePower;
        }

        private void Fire()
        {
            _bulletService.Fire(
                _customPhysics.GetTrajectoryData(_view.SpawnBulletPos.position, _view.SpawnBulletPos.forward,
                    _data.Power), _data.Power);
        }

        private void RotateCannon(Vector2 pointPos)
        {
            _view.RotateTo(pointPos);
            UpdateTrajectory();
        }

        private void UpdatePower(float sliderVal)
        {
            _data.UpdatePower(sliderVal);
            UpdateTrajectory();
        }

        private void UpdateTrajectory()
        {
            _view.TrajectoryView.UpdateTrajectory(_customPhysics.GetTrajectoryData(_view.SpawnBulletPos.position,
                _view.SpawnBulletPos.forward, _data.Power));
        }
    }
}