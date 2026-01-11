using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max = 100.0f;

    private float _minHealth = 0.0f;
    private float _currentHealth;

    public event Action DamageTaken;
    public event Action Healed;

    public bool IsAlive => _currentHealth > _minHealth;
    public float CurrentHealth => _currentHealth;
    public float Max => _max;

    private void Awake()
    {
        _currentHealth = _max;
    }

    public void Increase(float heal)
    {
        if (heal > 0)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + heal, _minHealth, _max);

            Healed?.Invoke();
        }
    }

    public void Decrease(float damage)
    {
        if (damage > 0)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _max);

            DamageTaken?.Invoke();
        }
    }
}