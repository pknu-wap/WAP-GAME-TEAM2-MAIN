using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTouch : MonoBehaviour
{
    private const float DAMAGE = 2f;
    public void MoveToTouch(Vector3 position)
    {
        transform.position = position;
    }
    private void OnTriggerEnter(Collider other)
    {
        Monster enemy = other.GetComponent<Monster>();
        Debug.Log(other.name);
        if (enemy != null)
        {
            enemy.GetComponent<EntityHealth>().TakeDamage(DAMAGE);
        }
    }
}
