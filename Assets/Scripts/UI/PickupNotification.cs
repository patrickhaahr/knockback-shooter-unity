using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class PickupNotification : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    
    private Label notificationLabel;
    private Coroutine hideCoroutine;
    
    private void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();
    }
    
    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;
        notificationLabel = root.Q<Label>("pickup-notification");
        
        if (notificationLabel != null)
        {
            notificationLabel.style.display = DisplayStyle.None;
        }
        
        Pickups.OnPickupCollected += ShowNotification;
    }
    
    private void OnDisable()
    {
        Pickups.OnPickupCollected -= ShowNotification;
    }
    
    private void ShowNotification(string message)
    {
        if (notificationLabel == null)
            return;
        
        notificationLabel.text = message;
        notificationLabel.style.display = DisplayStyle.Flex;
        
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        
        hideCoroutine = StartCoroutine(HideAfterDelay(2f));
    }
    
    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (notificationLabel != null)
        {
            notificationLabel.style.display = DisplayStyle.None;
        }
    }
}
