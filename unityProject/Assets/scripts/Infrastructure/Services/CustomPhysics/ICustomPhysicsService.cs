using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace Infrastructure.Services.CustomPhysics
{
    public interface ICustomPhysicsService : IService
    {
        public TrajectoryData GetTrajectoryData(Vector3 startPos, Vector3 direction, float power);
        public void Init(CannonStaticData getStaticData);
    }
}