using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AAttackBehaviour), typeof(EntityHealth))]
public class Tower : MonoBehaviour
{
    private AAttackBehaviour attackBehaviour;
    private EntityHealth healthInfo;

    [SerializeField]
    private float enemySearchingTerm;

    private void OnEnable()
    {
        attackBehaviour = GetComponent<AAttackBehaviour>();
        healthInfo = GetComponent<EntityHealth>();
        StartCoroutine(SeekEnemy());
    }

    private IEnumerator SeekEnemy()
    {
        var enemyList = EnemySpawnManager.Instance.SpawnObjectList;
        var searchingTerm = new WaitForSeconds(enemySearchingTerm);
        var distanceQueue = new PriorityQueueByDistance(transform.position);
        while (true)
        {
            distanceQueue.Clear();
            for (int i = 0; i < enemyList.Count; i++)
                if (enemyList[i] != null) distanceQueue.Enqueue(enemyList[i].transform);

            attackBehaviour.OrderAttack(distanceQueue.Dequeue().gameObject);
            yield return searchingTerm;
        }
    }
   
}
