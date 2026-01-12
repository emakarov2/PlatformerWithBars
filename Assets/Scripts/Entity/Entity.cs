using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
   protected Health Health;

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
    }

    public virtual void AcceptAttack(float damage)
    {
        Health.Decrease(damage);
    }
}