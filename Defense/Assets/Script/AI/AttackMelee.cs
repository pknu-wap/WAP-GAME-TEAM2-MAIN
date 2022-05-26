using System.Collections;
using UnityEngine;

public class AttackMelee : AAttackBehaviour
{

    override protected void Attack(GameObject target)
    {
        target.GetComponent<EntityHealth>().TakeDamage(damage);
    }
}


