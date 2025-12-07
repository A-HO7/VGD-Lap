using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public int score = 0;
    // We assume LevelOne is build index 1, LevelTwo is 2, etc.
    private int currentLevelIndex = 1;
    // UI Manager variable will be set in the inspector (see section 2)
    public UIManager uiManager;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ADD THIS: Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- Core Game Flow ---

    public void StartGame()
    {
        // Load the game from the current saved progress (LevelOne index 1 by default)
        SceneManager.LoadScene(currentLevelIndex);
    }

    // Call this before loading a new scene to unfreeze time
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreeze time

        if (uiManager != null)
        {
            uiManager.ShowGameHUD(); // This must hide the PauseMenuPanel
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Freeze time!
        // Show the pause menu panel (UI Manager handles this)
        if (uiManager != null)
        {
            uiManager.ShowPauseMenu();
        }
    }

    public void LoadLevelByIndex(int buildIndex)
    {
        // IMPORTANT: Always resume time before loading a new scene
        ResumeGame();
        if (buildIndex >= 1 && buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            currentLevelIndex = buildIndex;
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            Debug.LogError("Invalid level index.");
        }
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            currentLevelIndex = nextSceneIndex;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Game Finished! Going back to Main Menu.");
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        // IMPORTANT: Unfreeze time before exiting
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }

        // Optionally hide the pause menu panel explicitly before loading
        if (uiManager != null)
        {
            uiManager.ShowGameHUD();
        }

        // Load Scene 0 (Main Menu)
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        // Safety check: Ensure time is always running when a game scene is loaded.
        if (Time.timeScale != 1f && scene.buildIndex != 0)
        {
            Time.timeScale = 1f;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        // Editor only: 
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Your existing score method
    public void AddScore(int v)
    {
        score += v;
        Debug.Log("Score: " + score);
    }
}