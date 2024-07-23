using System;
using System.Collections;
using Characters;
using UnityEngine;

namespace Ability
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(EnemyDetected))]
    public class Vampiric : MonoBehaviour
    {
        [SerializeField] private float _healthRate = 10f;
        [SerializeField] private float _abilityDuration = 6f;
        [SerializeField] private float _cooldownDuration = 6f;
        [SerializeField] private EnemyDetected _enemyDetected;

        private float _currentCooldownDuration;
        private float _currentAbilityDuration;

        private SpriteRenderer _renderer;
        private CircleCollider2D _trigger;

        private Coroutine _coroutineVampiric;
        private Coroutine _coroutineCooldown;

        private bool _isAbilityActive;

        public event Action<float, float> ValueChanged;

        private void Start()
        {
            ValueChanged?.Invoke(_abilityDuration, _abilityDuration);

            _renderer = GetComponent<SpriteRenderer>();
            _trigger = GetComponent<CircleCollider2D>();

            _trigger.enabled = false;
            _renderer.enabled = false;
        }

        public void ActivationAbility()
        {
            if (_isAbilityActive == false && _coroutineCooldown == null)
            {
                ActivateComponent();

                _coroutineVampiric = StartCoroutine(TransferHealth());
            }
        }

        private void ApplyVampiric(float healthToTransfer, Entity enemy)
        {
            if (enemy == null)
                return;

            Player.Instance.Heal(healthToTransfer);
            enemy.TakeDamage(healthToTransfer);
        }

        private void StopAbility()
        {
            if (_coroutineVampiric != null)
            {
                StopCoroutine(_coroutineVampiric);
                _coroutineVampiric = null;
            }

            _coroutineCooldown = StartCoroutine(Cooldown());
        }

        private void StopCooldown()
        {
            if (_coroutineCooldown == null)
                return;

            StopCoroutine(_coroutineCooldown);

            _coroutineCooldown = null;
        }

        private void ActivateComponent()
        {
            _trigger.enabled = true;
            _renderer.enabled = true;
        }

        private void DeactivationComponent()
        {
            _trigger.enabled = false;
            _renderer.enabled = false;
        }

        private IEnumerator TransferHealth()
        {
            _isAbilityActive = true;

            _currentAbilityDuration = _abilityDuration;

            while (_currentAbilityDuration > 0)
            {
                _currentAbilityDuration -= Time.deltaTime;

                foreach (var entity in _enemyDetected.Enemy)
                {
                    ApplyVampiric(_healthRate * Time.deltaTime, entity);
                }

                ValueChanged?.Invoke(_currentAbilityDuration, _abilityDuration);

                yield return null;
            }

            _isAbilityActive = false;

            DeactivationComponent();

            StopAbility();
        }

        private IEnumerator Cooldown()
        {
            _currentCooldownDuration = 0;

            while (_currentCooldownDuration < _cooldownDuration)
            {
                _currentCooldownDuration += Time.deltaTime;

                ValueChanged?.Invoke(_currentCooldownDuration, _cooldownDuration);

                yield return null;
            }

            StopCooldown();
        }
    }
}