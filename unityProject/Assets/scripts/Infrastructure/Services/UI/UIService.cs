using System;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using Gameplay.UI;

namespace CodeBase.Infrastructure.Services.UI
{
    public class UIService : IUIService
    {
        public event Action<float> OnPowerChanged; 
        
        private readonly IGameFactory _gameFactory;
        private PowerPanel _powerPanel;

        public UIService() => 
            _gameFactory = AllServices.Container.Single<IGameFactory>();

        public void LoadPowerPanel()
        {
            _powerPanel = _gameFactory.Instantiate<PowerPanel>(AssetsAddress.PowerPanel);
            _powerPanel.OnPowerChanged += (x) => OnPowerChanged?.Invoke(x);
        }
    }
}