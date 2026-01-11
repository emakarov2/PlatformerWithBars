using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Health))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private string _isGrounded = "IsGrounded";
    [SerializeField] private string _verticalSpeed = "VerticalSpeed";
    [SerializeField] protected string _isRunning = "IsRunning";
    [SerializeField] protected string _isAttacking = "IsAttacking";
    [SerializeField] protected string _isAlive = "IsAlive";
    [SerializeField] protected string _damaged = "Damaged";

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private Health _health;

    private int _isGroundedHash;
    private int _verticalSpeedHash;
    private int _isRunningHash;
    private int _isAttackingHash;
    private int _isAliveHash;
    private int _damagedHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.DamageTaken += OnDamageTaken;
    }

    private void Start()
    {
        _isGroundedHash = Animator.StringToHash(_isGrounded);
        _verticalSpeedHash = Animator.StringToHash(_verticalSpeed);
        _isRunningHash = Animator.StringToHash(_isRunning);
        _isAttackingHash = Animator.StringToHash(_isAttacking);
        _isAliveHash = Animator.StringToHash(_isAlive);
        _damagedHash = Animator.StringToHash(_damaged);
    }

    private void Update()
    {
        SetupAnimator();
    }

    private void OnDisable()
    {
        _health.DamageTaken -= OnDamageTaken;
    }

    private void SetupAnimator()
    {
        bool isMoving = _inputReader.Direction != 0;
        bool isAttacking = _inputReader.IsAttacking;

        _animator.SetBool(_isGroundedHash, _groundDetector.IsGrounded);
        _animator.SetBool(_isRunningHash, isMoving);
        _animator.SetFloat(_verticalSpeedHash, _rigidbody.velocity.y);
        _animator.SetBool(_isAttackingHash, isAttacking);
        _animator.SetBool(_isAliveHash, _health.IsAlive);
    }

    private void OnDamageTaken()
    {
        _animator.SetTrigger(_damagedHash);
    }
}