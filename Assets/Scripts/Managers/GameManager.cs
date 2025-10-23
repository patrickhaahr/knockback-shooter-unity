using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawnerPrefab;
    
    public static GameManager Instance { get; private set; }
    
    private bool isGameOver = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        InitializeSpawner();
    }
    
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
    }
    
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void InitializeSpawner()
    {
        GameObject spawnerObj = GameObject.Find("EnemySpawner");
        
        if (spawnerObj == null)
        {
            spawnerObj = new GameObject("EnemySpawner");
            EnemySpawner spawner = spawnerObj.AddComponent<EnemySpawner>();
            
            if (enemyPrefab != null)
            {
                spawner.SetEnemyPrefab(enemyPrefab);
            }
            else
            {
                Debug.LogWarning("Enemy prefab not assigned to GameManager!");
            }
        }
    }
    
    private void HandlePlayerDeath()
    {
        if (!isGameOver)
        {
            isGameOver = true;
        }
    }
    
    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
