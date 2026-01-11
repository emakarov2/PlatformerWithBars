using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Berry : Item
{
    [SerializeField] private float _hpCount = 25;

    public float HpCount => _hpCount;
}