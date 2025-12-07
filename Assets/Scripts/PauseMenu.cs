using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    private int currentSceneIndex;

    void Start()
    {
        // Ensure the game manager is informed the game is NOW running and unpaused
        GameManager.Instance.ResumeGame(); // <-- Call this to reset TimeScale and hide UI panels

        isPaused = false; // Reset the local state
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        // Check for the Escape keypress
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // If already paused, unpause (Continue)
                ContinueGame();
            }
            else
            {
                // If not paused, pause the game
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        GameManager.Instance.PauseGame(); // Call the function to freeze time and show UI
    }

    public void ContinueGame()
    {
        isPaused = false;
        GameManager.Instance.ResumeGame(); // Call the function to unfreeze time and hide UI
    }

    public void RestartLevel()
    {
        // The GameManager handles loading, so we just pass the current index
        GameManager.Instance.LoadLevelByIndex(currentSceneIndex);
    }

    // This method is called by the button to go back to the Main Menu
    public void ReturnToMenu()
    {
        GameManager.Instance.LoadMenu();
    }
}