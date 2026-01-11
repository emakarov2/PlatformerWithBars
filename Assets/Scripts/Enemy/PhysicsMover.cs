using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMover : MonoBehaviour, IMover
{
    [SerializeField] private float _speed = 3f;

    private Rigidbody2D _rigidbody;

    private float _defaultSpeed;

    public bool IsDirectionDefault { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _defaultSpeed = _speed;
    }

    public void Move(Vector2 target)
    {
        float distanceX = target.x - transform.position.x;

        float directionX = Mathf.Sign(distanceX);
        _rigidbody.velocity = new Vector2(directionX * _speed, _rigidbody.velocity.y);

        IsDirectionDefault = directionX > 0;
    }

    public void Stop()
    {
        _speed = 0.0f;
    }

    public void Continue()
    {
        _speed = _defaultSpeed;
    }
}