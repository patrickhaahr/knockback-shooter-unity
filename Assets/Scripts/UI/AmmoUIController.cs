using UnityEngine;
using UnityEngine.UIElements;

public class AmmoUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private PlayerController playerController;

    private ProgressBar ammoBar;
    private Label ammoText;

    private void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();

        if (playerController == null)
            playerController = FindFirstObjectByType<PlayerController>();
    }

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        ammoBar = root.Q<ProgressBar>("ammo-bar");
        ammoText = root.Q<Label>("ammo-text");

        if (playerController != null)
        {
            UpdateAmmoDisplay();
        }
    }

    private void Update()
    {
        if (playerController != null)
        {
            UpdateAmmoDisplay();
        }
    }

    private void UpdateAmmoDisplay()
    {
        if (playerController != null)
        {
            int currentAmmo = playerController.GetCurrentAmmo();
            int maxAmmo = playerController.GetMaxAmmo();

            ammoBar.value = currentAmmo;
            ammoBar.highValue = maxAmmo;
            ammoText.text = $"{currentAmmo}/{maxAmmo}";
        }
    }
}
