using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    [SerializeField]
    private List<ObjectSpawner> spawnerList;
    [SerializeField]
    private List<EntityBaseInfo> entityInfoList;
    [SerializeField]
    private int enemyLayerMask = 15;

    [SerializeField]
    private int spawnerCountAtSpawnOrder;
    [SerializeField]
    private int minimumObjectSpawnCountPerSpawner;
    [SerializeField]
    private int maximumObjectSpawnCountPerSpawner;
    [SerializeField]
    private int maximumSpawnableObjectCount = 100;
    [SerializeField]
    private float spawnCheckPeriod = 10f;

    private List<GameObject> spawnedObjectList;
    private Dictionary<GameObject, int> indexPerSpawnGameObject;
    private Queue<int> nullReferenceIndexAtObjectList;

    public IReadOnlyList<GameObject> SpawnObjectList
    {
        get
        {
            if(spawnedObjectList == null)
                spawnedObjectList = new List<GameObject>(maximumSpawnableObjectCount);
            return spawnedObjectList.AsReadOnly();
        }
    }

    // TODO: determine is on work or not form PhaseManager
    private bool IsWorkable { get => PhaseManager.Instance.IsStart; }

    private void OnEnable()
    {
        if(spawnedObjectList == null)
            spawnedObjectList = new List<GameObject>(maximumSpawnableObjectCount);
        indexPerSpawnGameObject = new Dictionary<GameObject, int>(maximumSpawnableObjectCount);
        nullReferenceIndexAtObjectList = new Queue<int>(maximumSpawnableObjectCount);

        for (int i = 0; i < maximumSpawnableObjectCount; i++)
            spawnedObjectList.Add(null);

        for (int i = 0; i < maximumSpawnableObjectCount; i++)
            nullReferenceIndexAtObjectList.Enqueue(i);

        StartCoroutine(EnemySpawnProcess());
    }

    private IEnumerator EnemySpawnProcess()
    {
        var waitForPeriod = new WaitForSeconds(spawnCheckPeriod);
        while (true)
        {
            if(IsWorkable == false) yield return new WaitUntil(() => { return IsWorkable; });
            for(int i=0; i< spawnerCountAtSpawnOrder && i<spawnerList.Count;i++)
            {
                var spawnCount = Random.Range(minimumObjectSpawnCountPerSpawner, maximumSpawnableObjectCount + 1);
                var spawner = spawnerList[i];
                for(int j=0;j< spawnCount; j++)
                {
                    if (nullReferenceIndexAtObjectList.Count == 0) break;

                    var prefabIndex = Random.Range(0, entityInfoList.Count);
                    var prefab = entityInfoList[prefabIndex].EntityPrefab;
                    var spawnedObject = spawner.SpawnObject(prefab, enemyLayerMask);
                    InitSpawnedObject(spawnedObject, entityInfoList[prefabIndex]);
                }
            }
            yield return waitForPeriod;
        }
    }

    private void InitSpawnedObject(GameObject obj, EntityBaseInfo entityInfo)
    {
        // TODO : death event
        var entityHealth = obj.GetComponent<EntityHealth>();
        entityHealth.Init(entityInfo.HealthPoint, entityInfo.HealthPoint);
        var attackComponent = obj.GetComponent<AAttackBehaviour>();
        attackComponent.Init(entityInfo.Damage, entityInfo.Range, entityInfo.PreAttackDelay, entityInfo.PostAttackDelay);
        var moveComponent = obj.GetComponent<ArtificalMovement>();
        moveComponent.Init(entityInfo.MovementSpeed);

        var index = nullReferenceIndexAtObjectList.Dequeue();
        indexPerSpawnGameObject.Add(obj, index);
        spawnedObjectList[index] = obj;

        obj.GetComponent<EntityHealth>().AddEventOnDeath(DesposeDeadObjInList);
    }

    private void DesposeDeadObjInList(GameObject obj)
    {
        var index = indexPerSpawnGameObject[obj];
        spawnedObjectList[index] = null;
        nullReferenceIndexAtObjectList.Enqueue(index);
        indexPerSpawnGameObject.Remove(obj);
    } 
}
