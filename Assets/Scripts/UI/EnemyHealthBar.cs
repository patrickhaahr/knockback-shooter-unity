using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);

    private VisualElement healthBarFill;
    private Camera mainCamera;

    private void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();

        if (enemyHealth == null)
            enemyHealth = GetComponentInParent<EnemyHealth>();

        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            healthBarFill = root.Q<VisualElement>("health-bar-fill");
        }
    }

    private void LateUpdate()
    {
        if (enemyHealth != null && healthBarFill != null && mainCamera != null)
        {
            UpdateHealthBar();
            UpdatePosition();
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float)enemyHealth.GetCurrentHealth() / enemyHealth.GetMaxHealth();
        healthBarFill.style.width = Length.Percent(healthPercent * 100f);
    }

    private void UpdatePosition()
    {
        Vector3 worldPosition = transform.position + offset;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

        if (screenPosition.z > 0)
        {
            uiDocument.rootVisualElement.style.left = screenPosition.x;
            uiDocument.rootVisualElement.style.top = Screen.height - screenPosition.y;
        }
    }
}
