using UnityEngine;

[RequireComponent(typeof(PatrolBehaviour))]
[RequireComponent(typeof(ChaseBehaviour))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(IMover))]
[RequireComponent(typeof(PlayerDetector))]
[RequireComponent(typeof(Attacking))]
public class Enemy : Entity
{
    private PatrolBehaviour _patrol;
    private ChaseBehaviour _chase;
    private Flipper _flipper;
    private IMover _mover;
    private PlayerDetector _playerDetector;
    private Attacking _attacking;

    protected override void Awake()
    {
        base.Awake();
        _patrol = GetComponent<PatrolBehaviour>();
        _flipper = GetComponent<Flipper>();
        _mover = GetComponent<IMover>();
        _playerDetector = GetComponent<PlayerDetector>();
        _chase = GetComponent<ChaseBehaviour>();
        _attacking = GetComponent<Attacking>();        
    }

    private void Update()
    {
        if (_health.IsAlive)
        {
            if (_playerDetector.CanAttack)
            {
                _attacking.StartAttack();
            }
            else if (_playerDetector.IsDetected)
            {
                _attacking.StopAttack();
                _chase.Work();
            }
            else
            {
                _attacking.StopAttack();
                _patrol.Work();
            }

            _flipper.SetRotation(_mover.IsDirectionDefault);
        }
        else
        {
            _attacking.StopAttack();
        }
    }

   public override void AcceptAttack(float damage)
    {
        _health.Decrease(damage);        
    }
}