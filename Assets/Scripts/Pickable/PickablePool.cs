using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PickablePool : MonoBehaviour
{
    [SerializeField] private PickableItem _itemPrefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    
    private ObjectPool<PickableItem> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<PickableItem>(
            createFunc: Create,
            actionOnGet: (item) => item.gameObject.SetActive(true),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            actionOnDestroy: (item) => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private PickableItem Create()
    {
        PickableItem item = Instantiate(_itemPrefab);
        item.SetPool(this);
        return item;
    }

    public PickableItem GetItem()
    {
        return _pool.Get();
    }

    public void ReturnItem(PickableItem item)
    {
        _pool.Release(item);
    }
}
