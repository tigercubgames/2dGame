using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickablePool))]
public class PickableSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    
    private PickablePool _pickablePool;

    private void Awake()
    {
        _pickablePool = GetComponent<PickablePool>();
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
        PickableItem item = _pickablePool.GetItem();
        item.transform.position = spawnPoint.transform.position;
    }
}
