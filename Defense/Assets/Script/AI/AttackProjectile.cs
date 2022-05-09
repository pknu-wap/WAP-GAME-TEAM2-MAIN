using System.Collections;
using UnityEngine;

public class AttackProjectile : AAttackBehaviour
{

    private float FirePower;
    [SerializeField]
    private GameObject ProjectilePrefab;
    private Rigidbody Throw;
    [SerializeField]
    public GameObject Target;
    public Transform Pos;


    override protected void Attack(GameObject target)
    {
         Rigidbody throwerclone = (Rigidbody) Instantiate(Throw, transform.position, transform.rotation);
        Vector3 targetPos = target.transform.position - transform.position; 
        transform.rotation = Quaternion.LookRotation(targetPos); 
        throwerclone.AddForce(targetPos * 5000);
        GetComponent<Tower>().UpdateTarget;
    
        Destroy(Throw, 1f);
    }
 
}

