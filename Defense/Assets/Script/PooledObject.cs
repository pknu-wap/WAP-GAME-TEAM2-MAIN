using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private GameObject originPrefab = null;
    public GameObject OriginPrefab
    {
        set
        {
            if (originPrefab != null)
            {
                Debug.LogWarning("set origin prefab of pooled object is called more than twice at " + gameObject.name);
            }
            originPrefab = value;
        }
        get => originPrefab;
    }

    public void ReturnToPool()
    {
        ObjectPoolAdmin.Instance.ReturnObjectToPool(this);
    }
}
