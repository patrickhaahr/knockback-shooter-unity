using UnityEngine;
using System.Collections;

public class BossBattleManager : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private int killsToTriggerBoss = 10;
    [SerializeField] private float bossSpawnDistance = 10f;
    
    [Header("Pickup Settings")]
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private int pickupsToSpawn = 10;
    [SerializeField] private float pickupSpawnRadius = 5f;
    [SerializeField] private float pickupCollectionWindow = 5f;
    
    [Header("Camera Shake")]
    [SerializeField] private float shakeIntensity = 0.5f;
    [SerializeField] private float shakeDuration = 1f;
    
    [Header("Boss Scaling")]
    [SerializeField] private float bossScaleMultiplier = 2f;
    
    private Transform playerTransform;
    private CameraFollow cameraFollow;
    private int currentKillCount = 0;
    private bool bossSpawned = false;
    private GameObject currentBoss;
    private EnemySpawner enemySpawner;
    private int bossDefeatCount = 0;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraFollow = mainCamera.GetComponent<CameraFollow>();
        }
        
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        
        EnemyHealth.OnEnemyDestroyed += OnEnemyKilled;
        EnemyHealth.OnBossDestroyed += OnBossKilled;
    }

    private void OnDestroy()
    {
        EnemyHealth.OnEnemyDestroyed -= OnEnemyKilled;
        EnemyHealth.OnBossDestroyed -= OnBossKilled;
    }

    private void OnEnemyKilled()
    {
        if (bossSpawned)
        {
            return;
        }
        
        currentKillCount++;
        
        if (currentKillCount >= killsToTriggerBoss)
        {
            SpawnBoss();
        }
    }

    private void OnBossKilled()
    {
        OnBossDefeated();
    }

    private void SpawnBoss()
    {
        if (bossSpawned || bossPrefab == null || playerTransform == null)
            return;
        
        bossSpawned = true;
        
        if (cameraFollow != null)
        {
            StartCoroutine(cameraFollow.Shake(shakeIntensity, shakeDuration));
        }
        
        DespawnAllEnemies();
        
        if (enemySpawner != null)
        {
            enemySpawner.enabled = false;
        }
        
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = playerTransform.position + (Vector3)(spawnDirection * bossSpawnDistance);
        currentBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        
        ScaleBossStats();
    }

    private void DespawnAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            if (enemy != currentBoss)
            {
                Destroy(enemy);
            }
        }
    }

    private void OnBossDefeated()
    {
        bossSpawned = false;
        bossDefeatCount++;
        
        SpawnPickups();
        
        StartCoroutine(ResumeWaveAfterDelay());
    }
    
    private void ScaleBossStats()
    {
        if (currentBoss == null || bossDefeatCount == 0)
            return;
        
        float scaleFactor = Mathf.Pow(bossScaleMultiplier, bossDefeatCount);
        
        BossAI bossAI = currentBoss.GetComponent<BossAI>();
        if (bossAI != null)
        {
            bossAI.ScaleStats(scaleFactor);
        }
        
        EnemyHealth bossHealth = currentBoss.GetComponent<EnemyHealth>();
        if (bossHealth != null)
        {
            int scaledHealth = Mathf.RoundToInt(bossHealth.GetMaxHealth() * scaleFactor);
            bossHealth.SetMaxHealth(scaledHealth);
        }
    }

    private void SpawnPickups()
    {
        if (pickupPrefab == null || playerTransform == null)
            return;
        
        Vector3 bossPosition = currentBoss != null ? currentBoss.transform.position : playerTransform.position;
        
        for (int i = 0; i < pickupsToSpawn; i++)
        {
            float angle = (360f / pickupsToSpawn) * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * pickupSpawnRadius;
            Vector3 spawnPosition = bossPosition + (Vector3)offset;
            
            Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator ResumeWaveAfterDelay()
    {
        yield return new WaitForSeconds(pickupCollectionWindow);
        
        if (enemySpawner != null)
        {
            enemySpawner.IncreaseDifficulty(2f);
            enemySpawner.ResetForNextWave();
            enemySpawner.enabled = true;
        }
        
        currentKillCount = 0;
    }
}
