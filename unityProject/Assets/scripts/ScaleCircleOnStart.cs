using System;
using DG.Tweening;
using UnityEngine;

public class ScaleCircleOnStart : MonoBehaviour
{
    public float speed;
    public float endScale;
    public void Start()
    {
        transform.DOScale(endScale, speed).OnComplete(() =>
            Destroy(gameObject));
    }
}

