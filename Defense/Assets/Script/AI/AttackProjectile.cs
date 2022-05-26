using System.Collections;
using UnityEngine;

public class AttackProjectile : AAttackBehaviour
{

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float projectileSpeedPerSec = 10f;


    override protected void Attack(GameObject target)
    {
        transform.LookAt(target.transform.position);

        var projectile = ObjectPoolAdmin.Instance.GetPooledObject(projectilePrefab);
        projectile.GetComponent<Projectile>().Init(projectileSpeedPerSec, damage, target.transform, gameObject.transform);
    }
}


