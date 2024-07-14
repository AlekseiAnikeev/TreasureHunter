using UnityEngine;
using UnityEngine.UI;

namespace Bar
{
    public class SliderHealthBar : HealthBar
    {
        [SerializeField] protected Slider _slider;

        private void Start()
        {
            _slider.minValue = 0;
            _slider.maxValue = 1;
        }

        protected override void SetHealth(float currentHealth)
        {
            _slider.value = currentHealth / _health.MaxHealth;
        }
    }
}