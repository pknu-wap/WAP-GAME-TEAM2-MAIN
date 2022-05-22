using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PooledObject))]
public class EntityHealth : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    public bool IsDead { get => currentHealth <= 0; }

    public delegate void OnDeath(GameObject deadObject);
    private event OnDeath onDeathEvent;

    public void AddEventOnDeath(OnDeath deathEvent)
    {
        foreach (var func in onDeathEvent.GetInvocationList())
            if (func.Equals(deathEvent)) return;

        onDeathEvent += deathEvent;
    }

    public void Init(int maxHealth, int startHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = startHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage > maxHealth ? maxHealth : currentHealth - damage;
        // TODO damage taking animation
        if (currentHealth <= 0)
        {
            // TODO death animation
            StartCoroutine(ReturnToPool(1f));
        }
    }

    public IEnumerator ReturnToPool(float delay)
    {
        yield return new WaitForSeconds(delay);

        onDeathEvent(gameObject);
        GetComponent<PooledObject>().ReturnToPool();
    }
}

