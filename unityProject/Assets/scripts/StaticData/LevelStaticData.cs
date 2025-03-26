using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public Vector3 CannonSpawnPos { get; private set; }
        [field: SerializeField] public Vector3 ObstacleSpawnPos { get; private set; }
    }
}