using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoSingleton<Tower>
{
    public int Damage;
    public float Range; //범위 설정
    public GameObject Target;
    public Animator anim;
    public GameObject Splash;
    public GameObject Eff_obj;
    public GameObject Eff_pos;
    public Transform PartToRotate;
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget",0f, 0.2f);
        
    }
   
    private void Update() {
       {
           LookAtTarget();
       }
   }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, Range);
    }
    void UpdateTarget()
    {
        if (Target == null)
        {
            GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
            float shortestDistance = Mathf.Infinity; //가장 짧은거리
            GameObject nearestMonster = null; //가장 가까운 몬스터
            foreach (GameObject Monster in Monsters)
            {
                float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position);

                if (DistanceToMonsters < shortestDistance)
                {
                    shortestDistance = DistanceToMonsters;
                    nearestMonster = Monster;
                }
            }
            if (nearestMonster != null && shortestDistance <= Range)
            {
                Target = nearestMonster;
                ATTACK();
            }
            else
            {
                IDLE();
                Target = null;
            }
        }
        else if(Target != null)
        {
            float DistanceToMonsters = Vector3.Distance(transform.position, Target.transform.position);
            if (DistanceToMonsters > Range)
            {
                IDLE();
                Target = null;
            }
        }
    }

    public void ATTACK()
    {
        anim.SetInteger("TowerAnimState", 2);
    }
    public void IDLE()
    {
        anim.SetInteger("TowerAnimState", 1);
    }

    public void SplashDamage()
    {
        GameObject splash = Instantiate(Splash, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
        splash.GetComponent<Splash>().Damage = Damage;
        Destroy(splash, 2f); 

    }
    public void AttackTarget()
    {
        if (Target != null)
        {
            GameObject eff = Instantiate(Eff_obj, Eff_pos.transform.position, Quaternion.identity);
            Destroy(eff, 2f);
            Target.GetComponent<Monster>().GetDamage(Damage);
        }
    }

    void LookAtTarget()
    {
        if (Target == null)
        {
            return;
        }
        Vector3 dir = Target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
