using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "CannonStaticData", menuName = "StaticData/CannonData")]
    public class CannonStaticData : ScriptableObject
    {
        [field: SerializeField] public float BasePower { get; private set; }
        [field: SerializeField] public int Range { get; private set; }
        [field: SerializeField] public int BouncesAmount { get; private set; }
        [field: SerializeField] public float BounceDamping { get; private set; }
        [field: SerializeField] public float TimeStep { get; private set; }
        [field: SerializeField] public LayerMask CollisionLayers { get; private set; }
        [field: SerializeField] public BulletStaticData BulletStaticData { get; private set; }
        
    }
}