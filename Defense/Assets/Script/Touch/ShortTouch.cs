using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortTouch : MonoBehaviour
{
    private const float DAMAGE = 5f;
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }
    public void ShortAttack(Vector3 position)
    {
        StartCoroutine(GameManager.Instance.ParticleEffect(particle, position));
    }
    private void OnTriggerEnter(Collider other) {
        Monster enemy = other.GetComponent<Monster>();
        Debug.Log(other.name);
        if (enemy != null)
        {
            enemy.GetComponent<EntityHealth>().TakeDamage(DAMAGE);
        }
    }
}
