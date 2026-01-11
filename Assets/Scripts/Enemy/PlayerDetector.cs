using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _sightDistance = 25f;
    [SerializeField] private float _sightAngle = 120f;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _playerLayer;

    private float _attackDistance = 2f;

    public bool IsDetected { get; private set; }
    public bool CanAttack { get; private set; }

    private void Start()
    {
        IsDetected = false;
        CanAttack = false;
        StartCoroutine(FindTargetRoutine());
    }

    private IEnumerator FindTargetRoutine()
    {
        float delay = 0.1f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        while (enabled)
        {
            yield return waitForSeconds;

            IsDetected = CanSeePlayer(out float distance);
            CanAttack = (IsDetected && distance < _attackDistance);
        }
    }

    private Transform TryGetTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            _sightDistance,
            _playerLayer
            );

        if (hit == null)
        { return null; }
        else
        { return hit.transform; }
    }

    private bool IsInSector(Transform target, out Vector2 direction)
    {
        direction = target.position - transform.position;

        float sightAngleHalf = _sightAngle / 2;
        float angleToPlayer = Vector2.Angle(transform.right, direction);

        return angleToPlayer < sightAngleHalf;
    }

    private bool CanSeePlayer(out float distance)
    {
        distance = 0f;
        Transform target = TryGetTarget();

        if (target == null)
        { return false; }
        
        if (IsInSector(target, out Vector2 direction) == false)
        { return false; }

        distance = direction.magnitude;
        
        return Physics2D.Raycast(
            transform.position,
            direction,
            distance,
            _obstacleLayer).collider == null;
    }
}