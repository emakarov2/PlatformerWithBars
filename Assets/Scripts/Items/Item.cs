using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public event Action<Item> Collected;

    public void InvokeCollected()
    {
        Collected?.Invoke(this);
    }
}