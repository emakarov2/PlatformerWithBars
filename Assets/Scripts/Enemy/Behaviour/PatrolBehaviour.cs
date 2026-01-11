using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IMover))]
public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;

    private IMover _mover;

    private int _pointIndex = 0;
    private float _distanceInaccuracy = 1f;
    private float _delayAtBorder = 3f;
    private bool _isWaiting = false;

    private void Start()
    {
        _mover = GetComponent<PhysicsMover>();
    }

    public void Work()
    {
        if (_isWaiting) return;

        Transform pointByIndex = _wayPoints[_pointIndex];

        _mover.Move(pointByIndex.position);

        if (Mathf.Abs(transform.position.x - pointByIndex.position.x) < _distanceInaccuracy)
        {
            StartCoroutine(WaitAtPointReached());
            SetNextWayPoint();
        }
    }

    private IEnumerator WaitAtPointReached()
    {
        WaitForSeconds delay = new WaitForSeconds(_delayAtBorder);

        _isWaiting = true;
        _mover.Stop();

        yield return delay;

        _mover.Continue();
        _isWaiting = false;
    }

    private void SetNextWayPoint()
    {
        _pointIndex = ++_pointIndex % _wayPoints.Length;
    }
}