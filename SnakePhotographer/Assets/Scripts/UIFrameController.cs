using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIFrameController : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button photobookButton;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button mainMenuButton;

    [Header("UI Panels")]
    [SerializeField] private GameObject photobookPanel;
    [SerializeField] private GameObject mapPanel;

    [Header("Notes System")]
    [SerializeField] private TMP_InputField notesInputField;
    
    // Static ensures the text doesn't wipe when changing levels
    private static string savedNotes = ""; 

    [Header("Group Integration Events")]
    // This allows us to hook into your group's MainMenu() function via the Inspector
    public UnityEvent onMainMenuPressed;

    void Awake()
    {
        // Restore notes if the player already wrote some in a previous level
        if (notesInputField != null)
        {
            notesInputField.text = savedNotes;
            notesInputField.onValueChanged.AddListener(SaveNotesTemporarily);
        }

        // Setup simple panel toggles locally inside this script
        photobookButton.onClick.AddListener(TogglePhotobook);
        mapButton.onClick.AddListener(ToggleMap);

        // Setup Main Menu execution
        mainMenuButton.onClick.AddListener(HandleMainMenuClick);

        // Start with windows closed so they don't block gameplay
        photobookPanel.SetActive(false);
        mapPanel.SetActive(false);
    }

    void TogglePhotobook()
    {
        bool isActive = !photobookPanel.activeSelf;
        photobookPanel.SetActive(isActive);
        if (isActive) mapPanel.SetActive(false); // Clean overlap management
    }

    void ToggleMap()
    {
        bool isActive = !mapPanel.activeSelf;
        mapPanel.SetActive(isActive);
        if (isActive) photobookPanel.SetActive(false); // Clean overlap management
    }

    void SaveNotesTemporarily(string newText)
    {
        savedNotes = newText;
    }

      void HandleMainMenuClick()
    {
        if (notesInputField != null) savedNotes = notesInputField.text;

        // 1. ADD THIS LINE: Safely unloads your UI layout so it disappears from the screen
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("UIScene");

        // 2. This runs your group's MainMenu() function to switch scenes
        if (onMainMenuPressed != null)
        {
            onMainMenuPressed.Invoke();
        }
    }

    void OnDestroy()
    {
        if (notesInputField != null)
        {
            notesInputField.onValueChanged.RemoveListener(SaveNotesTemporarily);
        }
    }
}