using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max = 100.0f;

    private float _min = 0.0f;
    private float _current;

    public event Action DamageTaken;
    public event Action Healed;

    public bool IsAlive => _current > _min;
    public float Current => _current;
    public float Max => _max;

    private void Awake()
    {
        _current = _max;
    }

    public void Increase(float heal)
    {
        if (heal > 0)
        {
            _current = Mathf.Clamp(_current + heal, _min, _max);

            Healed?.Invoke();
        }
    }

    public void Decrease(float damage)
    {
        if (damage > 0)
        {
            _current = Mathf.Clamp(_current - damage, _min, _max);

            DamageTaken?.Invoke();
        }
    }
}