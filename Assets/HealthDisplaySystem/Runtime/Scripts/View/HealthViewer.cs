using UnityEngine;

public abstract class HealthViewer : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected virtual void OnEnable()
    {
        if (Health != null)
        {
            Health.DamageTaken += OnHealthChanged;
            Health.Healed += OnHealthChanged;
        }
    }

    protected virtual void OnDisable()
    {
        if (Health != null)
        {
            Health.DamageTaken -= OnHealthChanged;
            Health.Healed -= OnHealthChanged;
        }
    }

    protected abstract void OnHealthChanged();
}
