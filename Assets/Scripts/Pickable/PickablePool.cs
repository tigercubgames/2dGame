using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PickablePool<T> : MonoBehaviour where T : PickableItem
{
    [SerializeField] private T _itemPrefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    
    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_itemPrefab),
            actionOnGet: (item) => item.gameObject.SetActive(true),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            actionOnDestroy: (item) => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    public T GetItem()
    {
        return _pool.Get();
    }

    public void ReturnItem(T item)
    {
        _pool.Release(item);
    }
}
