using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfinitieSpawner : MonoBehaviour
{
    [SerializeField] private Item _prefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnInterval = 4f;

    [SerializeField] private int _itemPoolCapacity = 5;
    [SerializeField] private int _itemPoolMaxSize = 5;

    private ObjectPool<Item> _pool;
    private Queue<Vector3> _activePoints = new Queue<Vector3>();
    private Dictionary<Item, Vector3> _nonActivePoints = new Dictionary<Item, Vector3>();

    private void Awake()
    {
        _pool = new ObjectPool<Item>(
            createFunc: () => CreateItem(),
            actionOnGet: (item) => item.gameObject.SetActive(true),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            actionOnDestroy: (item) => Destroy(item),
            collectionCheck: true,
            defaultCapacity: _itemPoolCapacity,
            maxSize: _itemPoolMaxSize);
    }

    private void Start()
    {
        foreach (Transform transform in _spawnPoints)
        {
            _activePoints.Enqueue(transform.position);
        }

        StartCoroutine(SpawnCoinsRoutine());
    }

    private void OnDisable()
    {
        _pool.Clear();
    }

    private void Destroy(Item item)
    {
        if (item != null)
        {
            item.Collected -= OnItemCollected;

            Destroy(item.gameObject);
        }
    }

    private Item CreateItem()
    {
        Item item = Instantiate(_prefab);
        item.gameObject.SetActive(false);
        item.Collected += OnItemCollected;

        return item;
    }

    private IEnumerator SpawnCoinsRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            yield return delay;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if (_activePoints.Count == 0)
        {
            return;
        }

        Vector3 spawmPosition = _activePoints.Dequeue();

        Item item = _pool.Get();
        item.transform.position = spawmPosition;

        _nonActivePoints[item] = spawmPosition;
    }

    private void OnItemCollected(Item item)
    {
        if (_nonActivePoints.TryGetValue(item, out Vector3 position))
        {
            _activePoints.Enqueue(position);
            _nonActivePoints.Remove(item);
        }

        _pool.Release(item);
    }
}