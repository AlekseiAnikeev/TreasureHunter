using System;
using System.Collections;
using UnityEngine;

public abstract class Entity : Health
{
    [SerializeField] private float _spellCooldown;

    public event Action TakeHit;

    public bool IsAlive => _currentHealth <= _minHealth;
    public bool CanSpellUse { get; protected set; }

    private Coroutine _curentCoroutime;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        TakeHit?.Invoke();
    }

    public void DestroyObject()
    {
        if (_curentCoroutime != null)
        {
            StopCoroutine(Cooldown());
        }

        Destroy(gameObject);
    }

    public void StartAttackCooldown()
    {
        if (CanSpellUse)
        {
            CanSpellUse = false;

            _curentCoroutime = StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(_spellCooldown);

        CanSpellUse = true;
    }
}