using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

[Serializable]
public class ObjectPool 
{
    private GameObject prefab;
    private int defaultPoolSize = 50;

    private Transform parent;

    private Queue<GameObject> availableObjectQueue = new Queue<GameObject>();
    public IReadOnlyList<GameObject> PooledObjectList { get => availableObjectQueue.ToArray(); }

    private ObjectPool()
    {
        Assert.IsTrue(true,"object pool is made by default constructor");
    }

    public ObjectPool(GameObject prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    public void FillPool()
    {
        for(int i=0;i<defaultPoolSize&& availableObjectQueue.Count < defaultPoolSize;i++)
            availableObjectQueue.Enqueue(InstantiateObject());
    }

    private GameObject InstantiateObject()
    {
        var newPooledObject = GameObject.Instantiate(prefab, parent);
        newPooledObject.SetActive(false);

        var pooledObjInfo = newPooledObject.GetComponent<PooledObject>();
        if (pooledObjInfo == null) pooledObjInfo = newPooledObject.AddComponent<PooledObject>();
        pooledObjInfo.OriginPrefab = prefab;

        return newPooledObject;
    }

    public GameObject GetPooledObject()
    {
        if (availableObjectQueue.Count == 0)
            FillPool();
        return availableObjectQueue.Dequeue();
    }

    public bool ReturnObjectToPool(PooledObject pooledObj)
    {
        if(pooledObj.OriginPrefab == prefab)
        {
            availableObjectQueue.Enqueue(pooledObj.gameObject);
            pooledObj.transform.SetParent(parent);
            pooledObj.gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
