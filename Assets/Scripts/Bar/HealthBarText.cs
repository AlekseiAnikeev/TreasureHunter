using TMPro;
using UnityEngine;

namespace Bar
{
    public class HealthBarText : HealthBar
    {
        [SerializeField] private TextMeshProUGUI _healthText;

        protected override void SetBarValue(float currentHealth)
        {
            _healthText.text = $"{currentHealth}/{_health.MaxHealth}";
        }
    }
}