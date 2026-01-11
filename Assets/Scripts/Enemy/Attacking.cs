using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class Attacking : MonoBehaviour
{
    [SerializeField] private float _delay = 1f;

    private Attack _attack;

    private Coroutine _coroutine;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _attack = GetComponent<Attack>();
    }

    private void Start()
    {
        IsAttacking = false;
    }

    public void StartAttack()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(StrikePerDelayRoutine());

            IsAttacking = true;
        }
    }

    public void StopAttack()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            IsAttacking = false;
        }
    }

    private IEnumerator StrikePerDelayRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;
            _attack.Strike();
        }
    }
}