using UnityEngine;

public class VictimDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private LayerMask _enemyes;

    private int _maxNumberEnemyesInRange = 10;
    private Collider2D[] _enemyesInRadius;

    private void Awake()
    {
        _enemyesInRadius = new Collider2D[_maxNumberEnemyesInRange];
    }

    public float Radius => _radius;

    public Entity FindNearestAliveEnemy()
    {
        Vector2 player = transform.position;

        int enemyCount = Physics2D.OverlapCircleNonAlloc(player, _radius, _enemyesInRadius, _enemyes);

        if (enemyCount == 0)
        {
            return null;
        }

    Entity nearestEnemy = null;
    float minDistance = _radius;

        for (int i = 0; i < enemyCount; i++)
        {
            var enemyCollider = _enemyesInRadius[i];
            if (enemyCollider == null) 
            {
                continue;
            }

            if(enemyCollider.TryGetComponent(out Entity enemy) && enemy.TryGetComponent(out Health health) && health.IsAlive)
            {
                float distance = Vector2.Distance(player, enemyCollider.transform.position);

                if (distance < minDistance)
                {
                    nearestEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
        
        return nearestEnemy;
    }
}