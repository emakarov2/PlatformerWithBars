using UnityEngine;

[RequireComponent(typeof(IMover))]

public class ChaseBehaviour : MonoBehaviour
{
    [SerializeField] Transform _player;

    private IMover _mover;

    private void Start()
    {
        _mover = GetComponent<PhysicsMover>();
    }

    public void Work()
    {
        Vector2 target = new Vector2(_player.position.x, _player.position.y);
        
        _mover.Move(target);
    }
}