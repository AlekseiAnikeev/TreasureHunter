using System.Collections;
using UnityEngine;

namespace Bar
{
    public class HealthBarSmooth : SliderHealthBar
    {
        [SerializeField] private float _delta = 1f;

        private Coroutine _activeCoroutine;

        private void Start()
        {
            _slider.minValue = 0;
            _slider.maxValue = 1;
        }

        protected override void SetHealth(float currentHealth)
        {
            float currentValue = currentHealth / _health.MaxHealth;

            if (_activeCoroutine != null)
                StopCoroutine(_activeCoroutine);

            _activeCoroutine = StartCoroutine(DrawFillingOfSlider(currentValue));
        }

        private IEnumerator DrawFillingOfSlider(float currentHealth)
        {
            float barValue = _slider.value;

            for (float i = 0; i < _slider.maxValue; i += Time.deltaTime / _delta)
            {
                _slider.value = Mathf.Lerp(barValue, currentHealth, i);

                yield return null;
            }

            _slider.value = currentHealth;
        }
    }
}