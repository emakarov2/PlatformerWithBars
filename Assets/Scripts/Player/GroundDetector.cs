using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private SpriteRenderer _spriteRenderer;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(GroundCheckRoutine());
    }

    private IEnumerator GroundCheckRoutine()
    {
        float delay = 0.1f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            IsGrounded = CheckGrounding();
            yield return wait;
        }
    }

    private bool CheckGrounding()
    {
        float horizontalSize = 0.8f;
        float verticalSize = 0.1f;
        float castDistance = 0.2f;
        float angle = 0f;

        float playerHalfHeight = _spriteRenderer.bounds.size.y / 2;

        Vector2 startPoint = transform.position + Vector3.down * playerHalfHeight;
        Vector2 box = new Vector2(horizontalSize, verticalSize);

        RaycastHit2D hit = Physics2D.BoxCast(
            startPoint,
            box,
            angle,
            Vector2.down,
            castDistance,
            _groundLayer
            );

        return hit.collider != null;
    }
}