using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private PlayerHealth playerHealth;

    private ProgressBar healthBar;
    private Label healthText;

    private void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();

        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        healthBar = root.Q<ProgressBar>("health-bar");
        healthText = root.Q<Label>("health-text");

        if (playerHealth != null)
        {
            PlayerHealth.OnHealthChanged += UpdateHealthDisplay;
            UpdateHealthDisplay();
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            PlayerHealth.OnHealthChanged -= UpdateHealthDisplay;
        }
    }

    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        healthBar.value = currentHealth;
        healthBar.highValue = maxHealth;
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    private void UpdateHealthDisplay()
    {
        if (playerHealth != null)
        {
            int currentHealth = playerHealth.GetCurrentHealth();
            int maxHealth = playerHealth.GetMaxHealth();
            UpdateHealthDisplay(currentHealth, maxHealth);
        }
    }
}
