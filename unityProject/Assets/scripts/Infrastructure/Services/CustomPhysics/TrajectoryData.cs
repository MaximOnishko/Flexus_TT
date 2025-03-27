using UnityEngine;

namespace Infrastructure.Services.CustomPhysics
{
    public struct TrajectoryData
    {
        public Vector3[] Points;
        public HitData[] HitDatas;
    
        public int FirstHitIndex;
        public int LastHitIndex;

        public TrajectoryData(int range, int bouncesAmount)
        {
            Points = new Vector3[range];
            HitDatas = new HitData[bouncesAmount];

            FirstHitIndex = 0;
            LastHitIndex = 0;
        }
    
        public struct HitData
        {
            public Vector2 UV;
            public int TrajectoryIndex;
            public Collider Collider;
        }
    }
}