using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Attacking))]
[RequireComponent(typeof(Health))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] protected string _isRunning = "IsRunning";
    [SerializeField] protected string _isAttacking = "IsAttacking";
    [SerializeField] protected string _damaged = "Damaged";
    [SerializeField] protected string _isAlive = "IsAlive";

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Attacking _attacking;
    private Health _health;

    private int _isRunningHash;
    private int _isAttackingHash;
    private int _isAliveHash;
    private int _damagedHash;

    private void Awake()
    {
        _attacking = GetComponent<Attacking>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.DamageTaken += OnDamageTaken;
    }

    private void Start()
    {
        _isRunningHash = Animator.StringToHash(_isRunning);
        _isAttackingHash = Animator.StringToHash(_isAttacking);
        _isAliveHash = Animator.StringToHash(_isAlive);
        _damagedHash = Animator.StringToHash(_damaged);
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void OnDisable()
    {
        _health.DamageTaken -= OnDamageTaken;
    }

    private void UpdateAnimations()
    {
        float minSpeed = 0.1f;
        bool isMoving = Mathf.Abs(_rigidbody.velocity.x) > minSpeed;

        _animator.SetBool(_isRunningHash, isMoving);
        _animator.SetBool(_isAliveHash, _health.IsAlive);
        _animator.SetBool(_isAttackingHash, _attacking.IsAttacking);
    }

    private void OnDamageTaken()
    {
        _animator.SetTrigger(_damagedHash);
    }
}