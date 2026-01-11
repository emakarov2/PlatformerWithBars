using UnityEngine;

[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(Collector))]
[RequireComponent(typeof(Attack))]
public class Player : Entity
{
    private Flipper _flipper;
    private InputReader _inputReader;
    private PlayerMover _mover;
    private GroundDetector _groundDetector;
    private Collector _collector;
    private Attack _attack;

    protected override void Awake()
    {
        base.Awake();
        _flipper = GetComponent<Flipper>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _groundDetector = GetComponent<GroundDetector>();
        _collector = GetComponent<Collector>();
        _attack = GetComponent<Attack>();
    }

    private void Update()
    {
        _flipper.SetRotation(_inputReader.IsDirectionDefault);
    }

    private void FixedUpdate()
    {
        if (_health.IsAlive)
        {
            if (_inputReader.Direction != 0)
            {
                _mover.MoveByX(_inputReader.Direction);
            }

            if (_inputReader.GetIsJump() && _groundDetector.IsGrounded)
            {
                _mover.Jump();
            }

            if (_inputReader.GetIsInteracting())
            {
                _collector.CollectBerry();
            }

            if (_inputReader.GetIsAttacking())
            {
                _attack.Strike();
            }
        }
    }

    public override void AcceptAttack(float damage)
    {
        _health.Decrease(damage);
    }
}