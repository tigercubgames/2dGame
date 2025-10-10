using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableSpawner<T> : MonoBehaviour where T : PickableItem
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    
    private PickablePool<T> _pickablePool;

    private void Awake()
    {
        _pickablePool = GetComponent<PickablePool<T>>();
    }

    private void Start()
    {
        SpawnAtAllPoints();
    }

    private void SpawnAtAllPoints()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            SpawnAtPoint(spawnPoint);
        }
    }

    private void SpawnAtPoint(SpawnPoint spawnPoint)
    {
        T item = _pickablePool.GetItem();
        item.transform.position = spawnPoint.transform.position;
    }
}
