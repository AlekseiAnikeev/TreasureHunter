using UnityEngine;

namespace Bar
{
    public abstract class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Health _health;

        private void OnEnable()
        {
            _health.ValueChanged += SetHealth;
        }

        private void OnDisable()
        {
            _health.ValueChanged -= SetHealth;
        }

        protected abstract void SetHealth(float currentHealth);
    }
}