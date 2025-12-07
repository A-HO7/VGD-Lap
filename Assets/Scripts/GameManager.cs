using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public int score = 0;
    // We assume LevelOne is build index 1, LevelTwo is 2, etc.
    private int currentLevelIndex = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void LoadLevelByIndex(int buildIndex)
    {
        // Check to prevent trying to load a non-existent scene
        if (buildIndex >= 1 && buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            currentLevelIndex = buildIndex; // Update saved progress
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            Debug.LogError("Attempted to load level index " + buildIndex + ", which is outside the build settings range.");
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
        SceneManager.LoadScene(0); // Assuming Main Menu is always index 0
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