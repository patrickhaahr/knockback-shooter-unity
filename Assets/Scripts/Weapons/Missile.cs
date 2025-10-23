using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 3f;
    
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        Debug.Log("Missile started, moving with speed: " + speed);
        
        if (rb != null)
        {
            rb.linearVelocity = transform.up * speed;
            Debug.Log("Missile velocity set to: " + rb.linearVelocity);
        }
        else
        {
            Debug.LogError("Missile missing Rigidbody2D!");
        }
        
        Destroy(gameObject, lifetime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Missile collided with: " + other.gameObject.name);
        
        // Ignore collision with player
        if (other.CompareTag("Player"))
        {
            return;
        }
        
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy! Applying damage: " + damage);
            
            // Apply damage to enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Enemy missing EnemyHealth component!");
            }
            
            Destroy(gameObject);
        }
    }
}

