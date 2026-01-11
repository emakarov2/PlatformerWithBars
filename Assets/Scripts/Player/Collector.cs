using System;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private List<Berry> _berriesNear = new List<Berry>();

    public event Action<Item> CollectedItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Gem gem))
        {
            CollectedItem?.Invoke(gem);
            gem.InvokeCollected();
        }

        if (collision.gameObject.TryGetComponent(out Berry berry))
        {
            if (_berriesNear.Contains(berry) == false)
            {
                _berriesNear.Add(berry);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Berry berry))
        {
            _berriesNear.Remove(berry);
        }
    }

    public void CollectBerry()
    {
        if (_berriesNear.Count > 0)
        {
            Berry berry = _berriesNear[0];
            berry.InvokeCollected();
            CollectedItem?.Invoke(berry);
        }
    }
}