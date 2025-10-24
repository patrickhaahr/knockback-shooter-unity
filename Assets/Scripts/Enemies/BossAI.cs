using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float maxForce = 8f;
    [SerializeField] private float orbitDistance = 12f;
    [SerializeField] private float orbitSpeed = 2f;
    
    [Header("Attack Settings")]
    [SerializeField] private GameObject enemyMissilePrefab;
    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float missileSpeed = 8f;
    
    [Header("Burst Fire")]
    [SerializeField] private int burstCount = 5;
    [SerializeField] private float burstDelay = 0.2f;
    
    [Header("Spread Shot")]
    [SerializeField] private int spreadCount = 7;
    [SerializeField] private float spreadAngle = 45f;
    
    [Header("Minion Spawn")]
    [SerializeField] private int minionSpawnCount = 3;
    [SerializeField] private float minionSpawnRadius = 3f;
    
    private Transform playerTransform;
    private Rigidbody2D rb;
    private float nextAttackTime;
    private int attackPattern = 0;
    
    private float baseMoveSpeed;
    private float baseMaxForce;
    private float baseAttackCooldown;
    private float baseMissileSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.linearDamping = 0.5f;
        }
        
        baseMoveSpeed = moveSpeed;
        baseMaxForce = maxForce;
        baseAttackCooldown = attackCooldown;
        baseMissileSpeed = missileSpeed;
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        
        nextAttackTime = Time.time + attackCooldown;
    }

    private void FixedUpdate()
    {
        if (playerTransform == null || rb == null)
            return;

        MoveTowardsPlayer();
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        if (Time.time >= nextAttackTime)
        {
            PerformAttack();
            nextAttackTime = Time.time + attackCooldown;
            attackPattern = (attackPattern + 1) % 3;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 toPlayer = (Vector2)(playerTransform.position - transform.position);
        Vector2 desiredVelocity = toPlayer.normalized * moveSpeed;
        Vector2 steering = Vector2.ClampMagnitude(desiredVelocity - rb.linearVelocity, maxForce);
        rb.AddForce(steering);
    }

    private void PerformAttack()
    {
        switch (attackPattern)
        {
            case 0:
                StartCoroutine(BurstFire());
                break;
            case 1:
                SpreadShot();
                break;
            case 2:
                SpawnMinions();
                break;
        }
    }

    private IEnumerator BurstFire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            if (playerTransform != null && enemyMissilePrefab != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                SpawnMissile(direction);
            }
            yield return new WaitForSeconds(burstDelay);
        }
    }

    private void SpreadShot()
    {
        if (playerTransform == null || enemyMissilePrefab == null)
            return;

        Vector2 baseDirection = (playerTransform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

        float startAngle = baseAngle - (spreadAngle / 2f);
        float angleStep = spreadAngle / (spreadCount - 1);

        for (int i = 0; i < spreadCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            float rad = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            SpawnMissile(direction);
        }
    }

    private void SpawnMinions()
    {
        if (minionPrefab == null)
            return;

        for (int i = 0; i < minionSpawnCount; i++)
        {
            float angle = (360f / minionSpawnCount) * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * minionSpawnRadius;
            Vector3 spawnPosition = transform.position + (Vector3)offset;
            
            Instantiate(minionPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void SpawnMissile(Vector2 direction)
    {
        GameObject missile = Instantiate(enemyMissilePrefab, transform.position, Quaternion.identity);
        
        EnemyMissile missileScript = missile.GetComponent<EnemyMissile>();
        if (missileScript != null)
        {
            missileScript.Initialize(direction);
        }
        
        Rigidbody2D missileRb = missile.GetComponent<Rigidbody2D>();
        if (missileRb != null)
        {
            missileRb.linearVelocity = direction * missileSpeed;
        }
    }
    
    public void ScaleStats(float scaleFactor)
    {
        moveSpeed = baseMoveSpeed * scaleFactor;
        maxForce = baseMaxForce * scaleFactor;
        missileSpeed = baseMissileSpeed * scaleFactor;
        attackCooldown = baseAttackCooldown / scaleFactor;
        
        burstCount = Mathf.RoundToInt(burstCount * scaleFactor);
        spreadCount = Mathf.RoundToInt(spreadCount * scaleFactor);
        minionSpawnCount = Mathf.RoundToInt(minionSpawnCount * scaleFactor);
    }
}
