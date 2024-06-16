using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;

    public event Action OnTakeHit;

    public bool IsAlive => _currentHealth <= 0;

    protected float _currentHealth;

    public void TakeDamage(float damage)
    {
        OnTakeHit?.Invoke();

        _currentHealth -= damage;

        Debug.Log(_currentHealth);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}