using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private float maximumSpawnPointDistance = 10f;
    [SerializeField]
    private int maximumCollideCheckCount = 10;
    [SerializeField]
    private float collideCheckBoxHalfSize = 0.5f;
    [SerializeField]
    private float pendingTimeForRecheckSpawn = 1f;

    private float lastSpawnTime = 0f;
    private bool isPending = false;

    public GameObject SpawnObject(GameObject prefab,int collideLayer)
    {
        if (isPending) return null;
        Vector3 spawnPosition;
        //bool isSpawnable = GetAvailableSpawnPosition(out spawnPosition, collideLayer);

        //if (!isSpawnable)
        //{
        //    StartCoroutine(PendForRecheckSpawn());
        //    return null;
        //}

        var spawnedObject = ObjectPoolAdmin.Instance.GetPooledObject(prefab);
        spawnedObject.transform.position = transform.position;
        spawnedObject.SetActive(true);
        spawnedObject.transform.SetParent(transform);
        lastSpawnTime = Time.time;

        return spawnedObject;
    }

    private bool GetAvailableSpawnPosition(out Vector3 position, int collideLayer)
    {
        for(int i = 0; i < maximumCollideCheckCount; i++)
        {
            var randomDirection = new Vector3(Random.Range(-1f, 1f), transform.position.y, Random.Range(-1f, 1f)).normalized;
            var randomDistance = Random.Range(0, maximumSpawnPointDistance);

            var spawnPosition = transform.position + randomDirection * randomDistance;
            if (Physics.CheckBox(spawnPosition, Vector3.one * collideCheckBoxHalfSize,Quaternion.identity, collideLayer)) continue;

            position = spawnPosition;
            return true;
        }

        position = Vector3.zero;
        return false;
    }

    private IEnumerator PendForRecheckSpawn()
    {
        isPending = true;
        yield return new WaitForSeconds(pendingTimeForRecheckSpawn);
        isPending = false;
    }
}
