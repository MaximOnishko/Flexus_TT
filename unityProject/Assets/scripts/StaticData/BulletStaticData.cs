using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "BulletStaticData", menuName = "StaticData/BulletData")]
    public class BulletStaticData : ScriptableObject
    {
        [field: SerializeField] public float PowerToSpeedMultiplier { get; private set; }
        [field: SerializeField] public float BulletMeshRandomOffset { get; private set; }
        [field: SerializeField] public Texture2D HitTexture { get; private set; }
        [field: SerializeField] public float MovingTimeStep { get; private set; }
        [field: SerializeField] public LayerMask HitLayer { get; private set; }
    }
}