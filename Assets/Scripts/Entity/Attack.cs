using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Attack : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    private SpriteRenderer _spriteRenderer;

    private float _range = 1;
    private float _damage = 10;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Strike()
    {
        Entity target = TryGetTarget();
        Debug.Log("поиск цели");

        if (target != null)
        {
        Debug.Log("цель найдена");
            target.AcceptAttack(_damage);
        }
    }

    private Entity TryGetTarget()
    {
        float playerHalfWidht = _spriteRenderer.bounds.size.y / 2;

        Vector2 rayStart = transform.position + transform.right * playerHalfWidht;

        RaycastHit2D hit = Physics2D.Raycast(
        rayStart,
        transform.right,
        _range,
        _targetLayer
              );

        if (hit.collider != null && hit.collider.TryGetComponent(out Entity component))
        {
            return component;
        }

        return null;
    }
}