using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
   [SerializeField] private UIDocument uiDocument;
   
   private VisualElement root;
   private Button playButton;
   
   private void Awake()
   {
      if (uiDocument == null)
      {
         uiDocument = GetComponent<UIDocument>();
      }
   }
   
   private void Start()
   {
      root = uiDocument.rootVisualElement;
      SetupUI();
   }
   
   private void SetupUI()
   {
      playButton = root.Q<Button>("PlayButton");
      
      if (playButton != null)
      {
         playButton.clicked += PlayGame;
      }
   }
   
   private void PlayGame()
   {
      SceneManager.LoadScene("GameScene");
   }
}
