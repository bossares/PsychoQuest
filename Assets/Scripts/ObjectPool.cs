using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private GameObject _container;
    private int _capacity;
    private List<T> _pool = new List<T>();
    private T _prefab;

    public ObjectPool(T prefab, GameObject container, int capacity)
    {
        _prefab = prefab;
        _container = container;
        _capacity = capacity;

        Initialize();
    }

    public bool TryGetObject(out T result)
    {
        result = _pool.FirstOrDefault(pullObject => pullObject.gameObject.activeSelf == false);

        return result != null;
    }

    public void DeactivateAllItems()
    {
        foreach (var item in _pool)
            item.gameObject.SetActive(false);
    }

    private void Initialize()
    {
        for (int i = 0; i < _capacity; i++)
        {
            T spawned = Object.Instantiate(_prefab, _container.transform);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }
}