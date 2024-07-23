using UnityEngine;
using UnityEngine.UI;

namespace Ability
{
    public class CooldownBar : MonoBehaviour
    {
        [SerializeField] private Vampiric _vampiric;
        [SerializeField] private Slider _slider;
        
        private void Start()
        {
            _slider.minValue = 0;
            _slider.maxValue = 1;
        }
        
        private void OnEnable()
        {
            _vampiric.ValueChanged += SetBarValue;
        }

        private void OnDisable()
        {
            _vampiric.ValueChanged -= SetBarValue;
        }

        private void SetBarValue(float currentValue, float duration)
        {
            _slider.value = currentValue / duration;
        }
    }
}