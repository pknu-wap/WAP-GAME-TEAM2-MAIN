using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolAdmin : MonoSingleton<ObjectPoolAdmin>
{
    [SerializeField]
    private List<GameObject> prefabList = new List<GameObject>();
    private Dictionary<GameObject, ObjectPool> poolDictionaryByPrefab= new Dictionary<GameObject, ObjectPool>();

    [ContextMenu("Init All Pool")]
    private void Initialize()
    {
        foreach(var prefab in prefabList)
        {
            if (poolDictionaryByPrefab.ContainsKey(prefab) == false)
            {
                var newPool = new ObjectPool(prefab, transform);
                poolDictionaryByPrefab.Add(prefab, newPool);
                newPool.FillPool();
            }
        }
    }

    public GameObject GetPooledObject(GameObject prefab)
    {
        if(poolDictionaryByPrefab.ContainsKey(prefab) == false)
        {
            prefabList.Add(prefab);
            Initialize();
        }

        return poolDictionaryByPrefab[prefab].GetPooledObject();
    }

    public bool ReturnObjectToPool(PooledObject pooledObj)
    {
        var originPrefab = pooledObj.OriginPrefab;
        if(originPrefab == null || poolDictionaryByPrefab.ContainsKey(originPrefab) == false)
        {
            Debug.LogWarning("there is pooled obj without pool prefab name "+originPrefab.name);
            return false;
        }

        poolDictionaryByPrefab[originPrefab].ReturnObjectToPool(pooledObj);
        return true;
    }

    private void Start()
    {
        Initialize();
    }


#if UNITY_EDITOR
    [SerializeField]
    private List<GameObject> allPooledObjectList = new List<GameObject>();

    [ContextMenu("Refresh Current Pooled Object List")]
    private void RefreshPooledObjectList()
    {
        allPooledObjectList.Clear();
        foreach(var pool in poolDictionaryByPrefab)
        {
            foreach(var pooledObj in pool.Value.PooledObjectList)
                allPooledObjectList.Add(pooledObj);
        }
    }
#endif
}
