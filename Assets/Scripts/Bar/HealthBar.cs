using UnityEngine;

namespace Bar
{
    public abstract class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Health _health;

        private void OnEnable()
        {
            _health.ValueChanged += SetBarValue;
        }

        private void OnDisable()
        {
            _health.ValueChanged -= SetBarValue;
        }

        protected abstract void SetBarValue(float currentValue);
    }
}