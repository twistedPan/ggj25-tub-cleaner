using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private bool _menuOpen = false;


    public string MainMenuSceneName;

    InputAction openIngameMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void ResetLevel()
    {
        // Reload the current scene
        Debug.Log("Resetting Level");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadMainMenu()
    {
        // Load the main menu scene
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene(MainMenuSceneName);
    }

    void Start()
    {
        openIngameMenu = InputSystem.actions.FindAction("Open Ingame Menu");
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (openIngameMenu.WasPressedThisFrame())
        {
            if (_menuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        void OpenMenu()
        {
            // Open the ingame menu
            Debug.Log("Opening Ingame Menu");
            _canvasGroup.alpha = 1;
            Cursor.visible = true;
            _canvasGroup.interactable = true;
            _menuOpen = true;
        }

        void CloseMenu()
        {
            // Close the ingame menu
            Debug.Log("Closing Ingame Menu");
            _canvasGroup.alpha = 0;
            Cursor.visible = false;
            _canvasGroup.interactable = false;
            _menuOpen = false;
        }

    }
}