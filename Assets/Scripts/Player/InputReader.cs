using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    private bool _isJump;
    private bool _isInteracting;
    private bool _isAttacking;
    private bool _isDirectionDefault = true;

    public bool IsDirectionDefault => _isDirectionDefault;
    public bool IsAttacking => _isAttacking;
    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Direction != 0)
        {
            _isDirectionDefault = Direction > 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            _isInteracting = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isAttacking = true;
        }
    }

    public bool GetIsAttacking() => GetBoolAsTrigger(ref _isAttacking);

    public bool GetIsInteracting() => GetBoolAsTrigger(ref _isInteracting);

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}