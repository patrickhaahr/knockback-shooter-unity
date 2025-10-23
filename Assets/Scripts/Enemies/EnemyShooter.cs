using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject enemyMissilePrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float missileSpeed = 5f;
    
    private Transform player;
    private float nextFireTime;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        nextFireTime = Time.time + fireRate;
    }

    private void Update()
    {
        if (player == null) return;

        LookAtPlayer();

        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void LookAtPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ShootAtPlayer()
    {
        if (enemyMissilePrefab == null)
        {
            Debug.LogWarning("EnemyMissile prefab not assigned!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Quaternion missileRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        GameObject missile = Instantiate(enemyMissilePrefab, transform.position, missileRotation);
        
        Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();
        if (missileRb != null)
        {
            missileRb.linearVelocity = direction * missileSpeed;
        }

        EnemyMissile missileScript = missile.GetComponent<EnemyMissile>();
        if (missileScript != null)
        {
            missileScript.Initialize(direction);
        }
    }
}
