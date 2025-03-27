using CodeBase.StaticData;
using UnityEngine;

namespace Infrastructure.Services.CustomPhysics
{
    public class CustomPhysicsService : ICustomPhysicsService
    {
        private readonly Vector3 _gravity = new(0, -9.81f, 0);

        private CannonStaticData _cannonStaticData;

        public void Init(CannonStaticData cannonStaticData)
        {
            _cannonStaticData = cannonStaticData;
        }

        public TrajectoryData GetTrajectoryData(Vector3 startPos, Vector3 direction, float power)
        {
            TrajectoryData trajectoryData = new TrajectoryData(_cannonStaticData.Range, _cannonStaticData.BouncesAmount)
            {
                LastHitIndex = _cannonStaticData.Range - 1
            };

            Vector3 velocity = direction.normalized * power;
            Vector3 currentPosition = startPos;
            int bounceCount = 0;

            for (int i = 0; i < trajectoryData.Points.Length; i++)
            {
                trajectoryData.Points[i] = currentPosition;
                Vector3 nextPosition = currentPosition + velocity * _cannonStaticData.TimeStep;

                if (bounceCount == _cannonStaticData.BouncesAmount)
                {
                    trajectoryData.LastHitIndex = i;
                    break;
                }

                if (bounceCount < _cannonStaticData.BouncesAmount && Physics.Raycast(currentPosition,
                        velocity.normalized, out RaycastHit hit,
                        (nextPosition - currentPosition).magnitude, _cannonStaticData.CollisionLayers))
                {
                    trajectoryData.Points[i] = hit.point;

                    AddHitData(hit, ref trajectoryData, bounceCount, i);

                    if (bounceCount == 0)
                        trajectoryData.FirstHitIndex = i;

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

            return trajectoryData;
        }

        private void AddHitData(RaycastHit collision, ref TrajectoryData trajectoryData, int bounceCount, int i)
        {
            Vector3 direction = (collision.point - Camera.main.transform.position).normalized;

            if (Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit hit, 100f,
                    _cannonStaticData.BulletStaticData.HitLayer))
            {
                trajectoryData.HitDatas[bounceCount] = new TrajectoryData.HitData
                {
                    UV = new Vector2(hit.textureCoord.x, 1 - hit.textureCoord.y),
                    TrajectoryIndex = i,
                    Collider = hit.collider
                };
            }
        }
    }
}