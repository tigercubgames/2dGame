using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableSpawner<T> : MonoBehaviour where T : PickableItem
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private bool _useRandomSpawn = false;
    [SerializeField] private int _spawnCount = 1;
    
    private PickablePool<T> _pickablePool;

    private void Awake()
    {
        _pickablePool = GetComponent<PickablePool<T>>();
    }

    private void Start()
    {
        if (_useRandomSpawn)
        {
            SpawnAtRandomPoints();
        }
        else
        {
            SpawnAtAllPoints();
        }
    }

    private void SpawnAtAllPoints()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            SpawnAtPoint(spawnPoint);
        }
    }
    
    private void SpawnAtRandomPoints()
    {
        if (_spawnPoints.Length == 0) return;
        
        int[] randomPoints = GetRandomPoints();
        
        foreach (int index in randomPoints)
        {
            SpawnAtPoint(_spawnPoints[index]);
        }
    }
    
    private int[] GetRandomPoints()
    {
        int count = Mathf.Min(_spawnCount, _spawnPoints.Length);
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            availableIndices.Add(i);
        }
        
        int[] points = new int[count];

        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            points[i] = availableIndices[randomIndex];
            availableIndices.RemoveAt(randomIndex);
        }
        
        return points;
    }

    private void SpawnAtPoint(SpawnPoint spawnPoint)
    {
        T item = _pickablePool.GetItem();
        item.transform.position = spawnPoint.transform.position;
    }
}
