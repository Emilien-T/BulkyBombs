using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : Controller<MainMenuController>
{
    public MenuButton StartButton;
    public MenuButton QuitButton;
    public MenuSlider VolumeSlider;
    private List<MenuButton> menuButtons;
    private int selectionIndex = 0;
    private MenuUI currentSelectedUI;
    private bool selectionChangeable = true;
    public List<TMP_Text> leaderboards = new();

    private void Start()
    {
        InputController.Instance.directionalControls += OnDirectionChanged;
        InputController.Instance.buttonAny += OnButtonDown;
        int i = 0;
        foreach (TMP_Text t in leaderboards) 
        {
            if (LeaderboardService.leaderboardAsList.Count > i) 
            {
                t.text = LeaderboardService.leaderboardAsList[i].name + " : " + LeaderboardService.leaderboardAsList[i].score;
            }
            i++;
        }
    }

    public void StartGame() 
    {
        selectionChangeable = false;
        // SceneController can deal with transitions
        SceneController.Instance.LoadScene("Gameplay");
    }
    public void QuitGame() 
    {
        Application.Quit();
    }


    // Dealing with input
    private void OnDirectionChanged(Vector2 dir) 
    {
        
    }
    private void OnButtonDown(bool isDown) 
    {
        StartGame();
    }
    private void SelectButton() 
    {
        currentSelectedUI.OnDeselected();
        currentSelectedUI = menuButtons[selectionIndex];
        currentSelectedUI.OnSelected();
    }

    private void OnDisable()
    {
        InputController.Instance.directionalControls -= OnDirectionChanged;
        InputController.Instance.buttonAny -= OnButtonDown;
    }
}
