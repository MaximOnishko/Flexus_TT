using DG.Tweening;
using UnityEngine;

public class CannonAnimator : MonoBehaviour
{
    public float punchStrength = 1f;
    public float duration = 0.1f;
    public int vibrato = 10;
    public float elasticity = 1f;

    public void PlayAttack()
    {
        DOTween.Kill(transform);
        DOTween.Sequence(transform.DOPunchPosition(-transform.forward * punchStrength, duration, vibrato,
            elasticity));
    }
}