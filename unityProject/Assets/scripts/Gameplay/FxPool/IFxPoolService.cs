using CodeBase.Infrastructure.Services;
using Gameplay.Bullet;
using UnityEngine;

namespace Gameplay.FxPool
{
    public interface IFxPoolService : IService
    {
        void Init();
        void ReturnFx(GameObject bulletView);
        GameObject GetFx(Vector3 at);
    }
}