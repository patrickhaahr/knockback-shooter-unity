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
        if (rb != null)
        {
            rb.linearVelocity = transform.up * speed;
        }
        
        Destroy(gameObject, lifetime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}

