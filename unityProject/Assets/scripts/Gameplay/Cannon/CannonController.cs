using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.UI;
using CodeBase.StaticData;
using OpenCover.Framework.Model;
using UnityEngine;

namespace Cannon
{
    public class CannonController
    {
        private readonly IInputService _inputService;
        private readonly ICustomPhysicsService _customPhysics;
        private readonly IUIService _uiService;
        
        private CannonView _view;
        private CannonData _data;

        public CannonController()
        {
            _uiService = AllServices.Container.Single<IUIService>();
            _inputService = AllServices.Container.Single<IInputService>();
            _customPhysics = AllServices.Container.Single<ICustomPhysicsService>();
            
            Subscribe();
        }

        public void Init(CannonView view,CannonStaticData staticData)
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
            _customPhysics.UpdateTrajectory(_view.SpawnBulletPos.position, _view.SpawnBulletPos.forward, _data.Power);
            _view.TrajectoryView.UpdateTrajectory(_customPhysics.TrajectoryData);
        }
    }
}