using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private int _poolSize;
    [SerializeField] protected Transform Container;
    private List<GameObject> _pool = new List<GameObject>();

    protected void Init(GameObject prefab)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            var newPrefab = Instantiate(prefab, Container);
            newPrefab.SetActive(false);
            _pool.Add(newPrefab);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.Find(a => a.activeSelf == false);
        return result != null;
    }

    protected bool TryReturnObject(GameObject go)
    {
        bool isContains = _pool.Contains(go);
        if (isContains)
            go.transform.SetParent(Container);

        return isContains;
    }
}
