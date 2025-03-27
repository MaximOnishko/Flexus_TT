using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "CameraShake", menuName = "StaticData/CameraShake")]
    public class CameraShakeStaticData : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; } = 0.5f;
        [field: SerializeField] public float Strength { get; private set; } = 0.3f;
        [field: SerializeField] public int Vibrato { get; private set; } = 10;
        [field: SerializeField] public float Randomness { get; private set; } = 90f;
    }
}