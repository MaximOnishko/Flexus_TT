using System;

namespace CodeBase.Infrastructure.Services.UI
{
    public interface IUIService : IService
    {
        void LoadPowerPanel();
        event Action<float> OnPowerChanged;
    }
}