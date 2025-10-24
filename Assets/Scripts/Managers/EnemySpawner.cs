using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnDistance = 15f;
    [SerializeField] private float minSpawnDistance = 10f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private float waveDelay = 5f;

    private float nextSpawnTime;
    private int currentWave = 1;
    private int enemiesSpawnedThisWave;
    private int enemiesKilledThisWave;
    private bool isSpawningWave = true;
    private float difficultyMultiplier = 1f;

private void Start()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Player not found! EnemySpawner requires a player reference.");
                enabled = false;
                return;
            }
        }
        
        nextSpawnTime = Time.time + spawnInterval;
        EnemyHealth.OnEnemyDestroyed += OnEnemyKilled;
    }

    private void OnDestroy()
    {
        EnemyHealth.OnEnemyDestroyed -= OnEnemyKilled;
    }

    private void Update()
    {
        if (isSpawningWave && Time.time >= nextSpawnTime && enemiesSpawnedThisWave < enemiesPerWave)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
            enemiesSpawnedThisWave++;

            if (enemiesSpawnedThisWave >= enemiesPerWave)
            {
                isSpawningWave = false;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab not assigned!");
            return;
        }

Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.SetMaxHealth((int)(enemyHealth.GetMaxHealth() * difficultyMultiplier));
        }
        
        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.SetMoveSpeed(enemyAI.GetMoveSpeed() * difficultyMultiplier);
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, spawnDistance);
        Vector2 spawnPosition = (Vector2)playerTransform.position + randomDirection * randomDistance;
        return spawnPosition;
    }

    private void OnEnemyKilled()
    {
        enemiesKilledThisWave++;

        if (enemiesKilledThisWave >= enemiesPerWave)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesPerWave += 10;
        enemiesSpawnedThisWave = 0;
        enemiesKilledThisWave = 0;

        StartCoroutine(StartSpawningAfterDelay());
    }

    private System.Collections.IEnumerator StartSpawningAfterDelay()
    {
        yield return new WaitForSeconds(waveDelay);
        isSpawningWave = true;
        nextSpawnTime = Time.time + spawnInterval;
    }

    public void SetEnemyPrefab(GameObject prefab)
    {
        enemyPrefab = prefab;
    }

    public void ResetForNextWave()
    {
        currentWave++;
        enemiesPerWave += 10;
        enemiesSpawnedThisWave = 0;
        enemiesKilledThisWave = 0;
        isSpawningWave = true;
        nextSpawnTime = Time.time + spawnInterval;
    }
    
    public void IncreaseDifficulty(float multiplier)
    {
        difficultyMultiplier *= multiplier;
    }
    
    public void SetSpawnCooldown(float cooldown)
    {
        nextSpawnTime = Time.time + cooldown;
    }
}
