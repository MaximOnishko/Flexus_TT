using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class PowerPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Slider _slider;

        public event Action<float> OnPowerChanged;
        
        private void Start() => 
            _slider.value = 0.5f;

        private void OnEnable() => 
            _slider.onValueChanged.AddListener(OnSliderValueChanged);

        private void OnDisable() => 
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);

        private void OnSliderValueChanged(float value)
        {
            OnPowerChanged?.Invoke(value);
            _text.text = $"{(int)(value * 100)}";
        } 
    }
}