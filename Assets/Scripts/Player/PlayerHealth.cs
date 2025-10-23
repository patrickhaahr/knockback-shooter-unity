using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    public static event Action OnPlayerDeath;
    public static event Action<int, int> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
        
        GameObject gameOverObj = GameObject.Find("GameOver");
        GameOver gameOver = gameOverObj?.GetComponent<GameOver>();
        if (gameOver != null)
        {
            gameOver.ShowGameOver();
        }
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();
        PlayerController playerController = GetComponent<PlayerController>();
        
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
        
        if (collider != null)
        {
            collider.enabled = false;
        }
        
        if (playerController != null)
        {
            playerController.enabled = false;
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
}
