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
         Rigidbody throwerclone = (Rigidbody) Instantiate(Throw, transform.position, Quaternion.identity);
        throwerclone.AddForce(transform.position * 5000);
    
        Destroy(Throw, 1f);
    }
 
}

