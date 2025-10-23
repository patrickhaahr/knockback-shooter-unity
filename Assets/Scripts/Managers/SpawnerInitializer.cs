using UnityEngine;

public class SpawnerInitializer : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    
    private void Awake()
    {
        GameObject spawnerObj = new GameObject("EnemySpawner");
        EnemySpawner spawner = spawnerObj.AddComponent<EnemySpawner>();
        
        if (enemyPrefab != null)
        {
            spawner.SetEnemyPrefab(enemyPrefab);
            Debug.Log("EnemySpawner initialized with enemy prefab");
        }
        else
        {
            Debug.LogWarning("Enemy prefab not assigned to SpawnerInitializer!");
        }
        
        Destroy(gameObject);
    }
}
