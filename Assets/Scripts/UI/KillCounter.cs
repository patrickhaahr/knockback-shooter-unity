using UnityEngine;
using UnityEngine.UIElements;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    
    private Label killCounterText;
    private int killCount = 0;

    private void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        killCounterText = root.Q<Label>("kill-counter-text");
        
        EnemyHealth.OnEnemyDestroyed += IncrementKillCount;
        UpdateKillCounterDisplay();
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDestroyed -= IncrementKillCount;
    }

    private void IncrementKillCount()
    {
        killCount++;
        UpdateKillCounterDisplay();
    }

    private void UpdateKillCounterDisplay()
    {
        if (killCounterText != null)
        {
            killCounterText.text = $"Kills: {killCount}";
        }
    }

    public int GetKillCount()
    {
        return killCount;
    }

    public void ResetKillCount()
    {
        killCount = 0;
        UpdateKillCounterDisplay();
    }
}
