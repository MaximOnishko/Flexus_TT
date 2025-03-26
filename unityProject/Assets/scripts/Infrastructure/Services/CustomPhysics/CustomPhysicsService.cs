using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

public interface ICustomPhysicsService : IService
{
    TrajectoryData TrajectoryData { get; }
    public void UpdateTrajectory(Vector3 startPos, Vector3 direction, float power);
    public void Init(CannonStaticData getStaticData);
}

public class TrajectoryData
{
    public Vector3[] Points;
    public HitData[] HitDatas;
    
    public int FirstHitIndex;
    public int LastHitIndex;

    public TrajectoryData(int range, int bouncesAmount)
    {
        Points = new Vector3[range];
        HitDatas = new HitData[bouncesAmount];
    }
    
    public class HitData
    {
        public Vector2 UV;
        public int TrajectoryIndex;
    }
}

public class CustomPhysicsService : ICustomPhysicsService
{
    private readonly Vector3 _gravity = new(0, -9.81f, 0);
    
    public TrajectoryData TrajectoryData =>
        _trajectoryData;
    
    private TrajectoryData _trajectoryData;
    private CannonStaticData _cannonStaticData;

    public void Init(CannonStaticData cannonStaticData)
    {
        _cannonStaticData = cannonStaticData;
        _trajectoryData = new TrajectoryData(cannonStaticData.Range, cannonStaticData.BouncesAmount);
    }

    public void UpdateTrajectory(Vector3 startPos, Vector3 direction, float power)
    {
        Vector3 velocity = direction.normalized * power;
        Vector3 currentPosition = startPos;
        int bounceCount = 0;

        for (int i = 0; i < _trajectoryData.Points.Length; i++)
        {
            _trajectoryData.Points[i] = currentPosition;
            Vector3 nextPosition = currentPosition + velocity * _cannonStaticData.TimeStep;

            if (bounceCount == _cannonStaticData.BouncesAmount)
            {
                _trajectoryData.LastHitIndex = i;
                break;
            }

            if (bounceCount < _cannonStaticData.BouncesAmount && Physics.Raycast(currentPosition, velocity.normalized, out RaycastHit hit,
                    (nextPosition - currentPosition).magnitude, _cannonStaticData.CollisionLayers))
            {
                _trajectoryData.Points[i] = hit.point;
                _trajectoryData.HitDatas[bounceCount] = new TrajectoryData.HitData
                {
                    UV = hit.textureCoord,
                    TrajectoryIndex = i
                };

                if (bounceCount == 0)
                    _trajectoryData.FirstHitIndex = i;
                
                velocity = Vector3.Reflect(velocity, hit.normal) * _cannonStaticData.BounceDamping;
                currentPosition = hit.point + hit.normal * 0.01f;
                bounceCount++;
            }
            else
            {
                currentPosition = nextPosition;
                velocity += _gravity * _cannonStaticData.TimeStep;
            }
        }
    }
}