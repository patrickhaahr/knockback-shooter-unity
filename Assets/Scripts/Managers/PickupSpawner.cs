using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private GameObject enemyShooterPrefab;
    [SerializeField] private float spawnRadiusMin = 5f;
    [SerializeField] private float spawnRadiusMax = 10f;
    [SerializeField] private float enemyShooterSpawnChance = 0.3f;

    private Transform player;
    private int killsSinceLastSpawnCheck = 0;
    private float currentSpawnChance = 0.30f;
    private const float baseSpawnChance = 0.30f;
    private const float chanceIncrement = 0.05f;
    private const int minKillsBeforeFirstSpawn = 3;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        SpawnPickup();
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDestroyed += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDestroyed -= HandleEnemyKilled;
    }

    private void HandleEnemyKilled()
    {
        killsSinceLastSpawnCheck++;

        if (killsSinceLastSpawnCheck < minKillsBeforeFirstSpawn)
        {
            return;
        }

        if (Random.value <= currentSpawnChance)
        {
            SpawnPickup();
            currentSpawnChance = baseSpawnChance;
        }
        else
        {
            currentSpawnChance += chanceIncrement;
        }
    }

    private void SpawnPickup()
    {
        if (player == null)
        {
            Debug.LogError("PickupSpawner: player is null!");
            return;
        }

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(spawnRadiusMin, spawnRadiusMax);

        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        Vector3 spawnPosition = player.position + (Vector3)offset;

        bool spawnEnemyShooter = Random.value <= enemyShooterSpawnChance && enemyShooterPrefab != null;

        if (spawnEnemyShooter)
        {
            GameObject enemyShooter = Instantiate(enemyShooterPrefab, spawnPosition, Quaternion.identity);
            
            Vector2 closeOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            Vector3 pickupPosition = spawnPosition + (Vector3)closeOffset;
            
            if (pickupPrefab != null)
            {
                GameObject pickup = Instantiate(pickupPrefab, pickupPosition, Quaternion.identity);
            }
        }
        else
        {
            if (pickupPrefab == null)
            {
                Debug.LogError("PickupSpawner: pickupPrefab is null!");
                return;
            }
            
            GameObject pickup = Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
