using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothSpeed = 0.2f;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private void LateUpdate()
    {
        float desiredX = Mathf.Clamp(_player.position.x, _minX, _maxX);
        float currentX = transform.position.x;

        float smoothedX = Mathf.Lerp(currentX, desiredX, _smoothSpeed);

        transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);
    }
}