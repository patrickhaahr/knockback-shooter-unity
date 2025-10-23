using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    
    private VisualElement root;
    private VisualElement gameOverPanel;
    private Button restartButton;
    private Button quitButton;
    private Label gameOverText;
    private bool isGameOver = false;
    
    private void Awake()
    {
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
        }
        
        if (uiDocument != null)
        {
            uiDocument.sortingOrder = 1000;
        }
    }
    
    private void Start()
    {
        root = uiDocument.rootVisualElement;
        SetupUI();
        HideGameOver();
    }
    
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += ShowGameOver;
    }
    
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= ShowGameOver;
    }
    
    private void SetupUI()
    {
        gameOverPanel = root.Q<VisualElement>("GameOverPanel");
        gameOverText = root.Q<Label>("GameOverText");
        restartButton = root.Q<Button>("RestartButton");
        quitButton = root.Q<Button>("QuitButton");
        
        if (restartButton != null)
        {
            restartButton.clicked += RestartGame;
        }
        
        if (quitButton != null)
        {
            quitButton.clicked += QuitGame;
        }
    }
    
    public void ShowGameOver()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.style.display = DisplayStyle.Flex;
            gameOverPanel.style.opacity = 1f;
            gameOverPanel.BringToFront();
            Time.timeScale = 0f;
        }
    }
    
    public void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.style.display = DisplayStyle.None;
        }
        isGameOver = false;
    }
    
    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void QuitGame()
    {
        Time.timeScale = 1f;
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
