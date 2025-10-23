using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isBoss = false;

    public static event Action OnEnemyDestroyed;
    public static event Action OnBossDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsBoss()
    {
        return isBoss;
    }
    
    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }

    private void Die()
    {
        if (isBoss)
        {
            OnBossDestroyed?.Invoke();
        }
        else
        {
            OnEnemyDestroyed?.Invoke();
        }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            
            if (playerController != null)
            {
                playerController.AddAmmo(1);
            }
            
            if (playerHealth != null)
            {
                playerHealth.Heal(10);
            }
        }
        
        Destroy(gameObject);
    }
}
