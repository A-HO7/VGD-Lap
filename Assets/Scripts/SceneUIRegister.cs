using UnityEngine;
using System.Collections; // Required for Coroutines

public class SceneUIRegister : MonoBehaviour
{
    // === Main Menu Scene (Scene 0) ===
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;

    // === Game Level Scene (Scene 1, 2, etc.) ===
    public GameObject pauseMenuPanel;

    void Awake()
    {
        // Start the initialization process immediately, but with a slight delay
        StartCoroutine(InitializeUIReferences());
    }

    private IEnumerator InitializeUIReferences()
    {
        // Wait one frame to ensure GameManager.Awake() (with DontDestroyOnLoad) has completed.
        yield return null;

        if (GameManager.Instance != null && GameManager.Instance.uiManager != null)
        {
            UIManager uiManager = GameManager.Instance.uiManager;

            // Check if this is the Main Menu Scene
            if (mainMenuPanel != null)
            {
                uiManager.SetMainMenuPanels(mainMenuPanel, levelSelectPanel);
                Debug.Log("UI Register: Main Menu references set.");
            }
            // Check if this is a Game Level Scene
            else if (pauseMenuPanel != null)
            {
                uiManager.SetGamePanels(pauseMenuPanel);
                Debug.Log("UI Register: Pause Menu reference set.");
            }
        }
        else
        {
            Debug.LogError("FATAL: GameManager Instance or UIManager reference is NULL upon scene load. Cannot initialize UI.");
        }
    }
}