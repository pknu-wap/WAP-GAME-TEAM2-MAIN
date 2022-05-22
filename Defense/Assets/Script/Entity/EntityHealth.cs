using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class EntityHealth : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    public bool IsDead { get => currentHealth <= 0; }

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
            ReturnToPool(1f);
        }
    }

    public void ReturnToPool(float delay)
    {
        GetComponent<PooledObject>().Invoke("Return To Pool", delay);
    }
}

