using System;
using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] private float _spellCooldown;

    public event Action TakeHit;

    public bool IsAlive => _currentHealth <= _minHealth;
    public bool CanSpellUse { get; protected set; }

    private Coroutine _curentCoroutime;
    protected float _currentHealth;
    protected float _minHealth = 0f;
    
    public void TakeDamage(float damage)
    {
        TakeHit?.Invoke();

        _currentHealth -= damage;

        Debug.Log(_currentHealth);
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