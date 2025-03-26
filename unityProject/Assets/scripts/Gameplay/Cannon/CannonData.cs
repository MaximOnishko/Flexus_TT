using UnityEngine;

public class CannonData
{
    public float Power { get; private set; }
    
    private readonly float _basePower;
    public CannonData(float basePower)
    {
        _basePower = basePower;
        Power = _basePower;
    }
    
    public void UpdatePower(float sliderVal)
    {
        Power = Mathf.Lerp(0, _basePower * 2f, sliderVal);
    }
}