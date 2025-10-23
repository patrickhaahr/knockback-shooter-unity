using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float separationRadius = 1.5f;
    [SerializeField] private float separationWeight = 2f;
    [SerializeField] private float maxForce = 10f;
    
    
    private Transform playerTransform;
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        
        if (rb != null)
        {
            rb.linearDamping = 0.5f;
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform == null || enemyHealth == null || rb == null)
            return;

        PursuePlayer();
    }

    private void PursuePlayer()
    {
        Vector2 playerDirection = (playerTransform.position - transform.position).normalized;
        Vector2 separation = CalculateSeparation();

        Vector2 desiredVelocity = (playerDirection + (separation * separationWeight)).normalized * moveSpeed;
        Vector2 steering = Vector2.ClampMagnitude(desiredVelocity - rb.linearVelocity, maxForce);
        
        rb.AddForce(steering);
    }

    private Vector2 CalculateSeparation()
    {
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, separationRadius);
        Vector2 separationForce = Vector2.zero;

        foreach (Collider2D neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject && neighbor.CompareTag("Enemy"))
            {
                Vector2 diff = (Vector2)transform.position - (Vector2)neighbor.transform.position;
                float distance = diff.magnitude;
                if (distance > 0)
                {
                    separationForce += diff.normalized / distance;
                }
            }
        }

        return separationForce.normalized;
    }
}
