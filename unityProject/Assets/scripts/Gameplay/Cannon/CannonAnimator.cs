using System;
using DG.Tweening;
using UnityEngine;

public class CannonAnimator : MonoBehaviour
{
    [Header("Scale Punch")] public Transform mesh;
    public Vector3 punchScale;

    [Header("Pos Punch")] public float punchStrength = 1f;
    public float duration = 0.1f;
    public int vibrato = 10;
    public float elasticity = 1f;

    private Vector3 _startScale;
    private Vector3 _startPos;

    private void Start()
    {
        _startScale = mesh.transform.localScale;
        _startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayAttack();
        }
    }

    public void PlayAttack()
    {
        mesh.transform.localScale = _startScale;
        transform.position = _startPos;
        
        DOTween.Kill(GetHashCode());
        DOTween.Sequence().SetId(GetHashCode())
            .Append(transform.DOPunchPosition(-transform.forward * punchStrength, duration, vibrato, elasticity))
            .Join(mesh.DOPunchScale(punchScale, duration, vibrato, elasticity));
    }
}