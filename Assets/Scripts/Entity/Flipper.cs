using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _defaultDirectionMultiplier = 1f;
    private float _flippedDirectionMultiplier = -1f;
    
    private bool _isFlipped = false;

    public bool IsFacingRight => _isFlipped == false;
    public float Direction => IsFacingRight ? _defaultDirectionMultiplier : _flippedDirectionMultiplier;

    public void SetRotation(bool isOrientDefault)
    {
        if (_isFlipped != isOrientDefault)
            return;

        _spriteRenderer.flipX = !isOrientDefault;
        _isFlipped = isOrientDefault == false;
    }
}