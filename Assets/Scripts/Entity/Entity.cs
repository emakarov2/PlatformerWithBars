using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
   protected Health _health;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }

    public virtual void AcceptAttack(float damage)
    {
        _health.Decrease(damage);
    }
}