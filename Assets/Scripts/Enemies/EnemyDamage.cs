using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int missileDamage = 10;
    [SerializeField] private int playerContactDamage = 10;
    [SerializeField] private float damageInterval = 1f;
    
    private EnemyHealth enemyHealth;
    private float lastDamageTime = -999f;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile"))
        {
            
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(missileDamage);
            }
            else
            {
                Debug.LogWarning("Enemy missing EnemyHealth component!");
            }
        }
        
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerContactDamage);
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageInterval)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(playerContactDamage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
