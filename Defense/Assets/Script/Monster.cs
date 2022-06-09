using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AAttackBehaviour), typeof(ArtificalMovement), typeof(EntityHealth))]
public class Monster : MonoBehaviour
{
    private AAttackBehaviour attackBehaviour;
    private EntityHealth healthInfo;
    private ArtificalMovement movementComponent;


    [SerializeField]
    private float enemySearchingTerm;

    private void OnEnable()
    {
        attackBehaviour = GetComponent<AAttackBehaviour>();
        healthInfo = GetComponent<EntityHealth>();
        movementComponent = GetComponent<ArtificalMovement>();
        StartCoroutine(Combat());
    }
    private IEnumerator Combat()
    {
        var towerList = FieldManager.Instance.towerList;
        var searchingTerm = new WaitForSeconds(enemySearchingTerm);
        var distanceQueue = new PriorityQueueByDistance(transform.position);
        while (true)
        {
            distanceQueue.Clear();
            for (int i = 0; i < towerList.Count; i++)
                if (towerList[i] != null) distanceQueue.Enqueue(towerList[i].transform);

            GameObject closestEnemy =  distanceQueue.Count > 0 ? distanceQueue.Dequeue().gameObject : null;
            if (closestEnemy != null)
            {
                var distanceToEnemy = (transform.position - closestEnemy.transform.position).magnitude;

                if (distanceToEnemy > movementComponent.stoppingDistance) movementComponent.MoveTo(closestEnemy.transform);
                else attackBehaviour.OrderAttack(closestEnemy);
            }

            yield return searchingTerm;
        }
    }

}
