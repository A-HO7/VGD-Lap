using UnityEngine;
using UnityEngine.UI; // Often required for UI elements

public class UIManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;    // Drag MainMenuPanel here
    public GameObject levelSelectPanel; // Drag LevelSelectPanel here

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

    public void ShowLevelSelect()
    {
        if (mainMenuPanel)
            mainMenuPanel.SetActive(false);
        if (levelSelectPanel)
            levelSelectPanel.SetActive(true);
    }
}