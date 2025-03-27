using UnityEngine;

public class CannonData
{
    public float Power { get; private set; }

    public float BulletSpeed =>
        Power * _speedMultiplier;

    private readonly float _basePower;
    private readonly float _speedMultiplier;

    public CannonData(float basePower, float speedMultiplier)
    {
        _speedMultiplier = speedMultiplier;
        _basePower = basePower;
        Power = _basePower;
    }
    
    public void UpdatePower(float sliderVal)
    {
        Power = Mathf.Lerp(0, _basePower * 2f, sliderVal);
    }
}