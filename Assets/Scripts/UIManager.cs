using UnityEngine;
using UnityEngine.UI; // Often required for UI elements

public class UIManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;    // Drag MainMenuPanel here
    public GameObject levelSelectPanel; // Drag LevelSelectPanel here
    [Header("In-Game Panels")]
    public GameObject pauseMenuPanel;
    void Start()
    {
        // Ensure the main menu is visible and others are hidden on start
        ShowMainMenu();
    }

    // --- Menu Navigation Functions ---

    public void ShowMainMenu()
    {
        if (mainMenuPanel)
            mainMenuPanel.SetActive(true);
        if (levelSelectPanel)
            levelSelectPanel.SetActive(false);
    }
    public void ShowPauseMenu()
    {
        // gameHUD.SetActive(false); // Optionally hide the HUD
        pauseMenuPanel.SetActive(true);
    }
    public void SetMainMenuPanels(GameObject mainMenu, GameObject levelSelect)
    {
        mainMenuPanel = mainMenu;
        levelSelectPanel = levelSelect;

        // Ensure the correct panel is shown after assignment
        ShowMainMenu();

        Debug.Log("UIManager: Main Menu Panels successfully reassigned.");
    }
    public void SetGamePanels(GameObject pauseMenu)
    {
        pauseMenuPanel = pauseMenu;
        Debug.Log("UIManager: Pause Menu Panel successfully reassigned.");
    }
    public void ShowLevelSelect()
    {

        if (mainMenuPanel)
            mainMenuPanel.SetActive(false);
        if (levelSelectPanel)
            levelSelectPanel.SetActive(true);
    }
    // Update the main visibility function (called by GameManager.ResumeGame())
    public void ShowGameHUD()
    {
        // gameHUD.SetActive(true);
        if (pauseMenuPanel) pauseMenuPanel.SetActive(false);
        // ...
    }
}