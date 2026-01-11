using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collector))]
public class ItemHandler : MonoBehaviour
{
    private Health _health;
    private Collector _collector;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _collector = GetComponent<Collector>();
    }

    private void OnEnable()
    {
        _collector.CollectedItem += OnCollectedItem;
    }

    private void OnDisable()
    {
        _collector.CollectedItem -= OnCollectedItem;
    }

    private void OnCollectedItem(Item coin)
    {
        if (coin != null && coin.gameObject.TryGetComponent(out Berry berry))
        {
            HandleBerry(berry);
        }
    }

    private void HandleBerry(Berry berry)
    {
        _health.Increase(berry.HpCount);
    }
}