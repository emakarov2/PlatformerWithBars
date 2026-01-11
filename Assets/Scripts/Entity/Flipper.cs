using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flipper : MonoBehaviour
{
    private Quaternion _defaultRotation = Quaternion.identity;
    private Quaternion _rotatedByY = Quaternion.Euler(0f, 180f, 0f);

    private bool _isFlipped = false;

    public void SetRotation(bool isOrientDefault)
    {
        if(_isFlipped != isOrientDefault) 
            return;
        
        transform.rotation = isOrientDefault ? _defaultRotation : _rotatedByY;
        _isFlipped = isOrientDefault == false;
    }
}