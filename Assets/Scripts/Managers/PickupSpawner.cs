using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private GameObject enemyShooterPrefab;
    [SerializeField] private float spawnRadiusMin = 5f;
    [SerializeField] private float spawnRadiusMax = 10f;

    private Transform player;
    private int killsSinceLastSpawnCheck = 0;
    private float currentSpawnChance = 0.60f;
    private const float baseSpawnChance = 0.60f;
    private const float chanceIncrement = 0.05f;
    private const int minKillsBeforeFirstSpawn = 2;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>().transform;
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

        if (pickupPrefab == null)
        {
            Debug.LogError("PickupSpawner: pickupPrefab is null!");
            return;
        }

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(spawnRadiusMin, spawnRadiusMax);

        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        Vector3 pickupPosition = player.position + (Vector3)offset;

        GameObject pickup = Instantiate(pickupPrefab, pickupPosition, Quaternion.identity);
        
        if (pickup != null && enemyShooterPrefab != null)
        {
            Vector2 closeOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            Vector3 enemyPosition = pickupPosition + (Vector3)closeOffset;
            GameObject enemyShooter = Instantiate(enemyShooterPrefab, enemyPosition, Quaternion.identity);
        }
    }
}
