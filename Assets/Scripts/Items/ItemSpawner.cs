using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _prefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnInterval = 4f;

    private void Start()
    {
        StartCoroutine(SpawnCoinsRoutine());
    }

    private IEnumerator SpawnCoinsRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            yield return delay;
            Work();
        }
    }

    private void Work()
    {
        if (_spawnPoints.Count == 0)
        {
            StopSpawning();
            return;
        }

        int randomIndex = Random.Range(0, _spawnPoints.Count);
        Transform spawnPoint = _spawnPoints[randomIndex];

        _spawnPoints.RemoveAt(randomIndex);

        Item item = Instantiate(_prefab, spawnPoint.position, Quaternion.identity);

        item.Collected += Destroy;
    }

    private void Destroy(Item item)
    {
        item.Collected -= Destroy;

        Destroy(item.gameObject);
    }

    private void StopSpawning()
    {
        StopCoroutine(SpawnCoinsRoutine());
        enabled = false;
    }
}