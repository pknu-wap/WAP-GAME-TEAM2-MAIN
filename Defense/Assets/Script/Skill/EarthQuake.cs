using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : APlayerSkill
{
    private Monster[] enemies;
    private BoxCollider coli;
    private Vector3 size;
    const int DAMAGE = 100;
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.position = new Vector3(0, 1, 0);
        enemies = FindObjectsOfType<Monster>();
        coli = GetComponent<BoxCollider>();

    }
    public override void Use()
    {
        coolTime = 5f;
        if(particle != null)
        {
            StartCoroutine(ParticleEffect(particle));
        }
    }
    public void Update()
    {
        size += new Vector3(1, 1, 1) * 5;
        coli.size = size;

    }
    
    private void OnTriggerEnter(Collider other)
    {
        Monster enemy = other.GetComponent<Monster>();
        Debug.Log(other.name);
        if(enemy != null)
        {
            enemy.GetComponent<EntityHealth>().TakeDamage(DAMAGE);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌");
    }
}
