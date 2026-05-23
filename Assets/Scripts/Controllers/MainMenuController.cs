using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuController : Controller<MainMenuController>
{
    public MenuButton StartButton;
    public MenuButton QuitButton;
    public MenuSlider VolumeSlider;
    private List<MenuButton> menuButtons;
    private int selectionIndex = 0;
    private MenuUI currentSelectedUI;
    private bool selectionChangeable = true;

    private void Start()
    {
        InputController.Instance.directionalControls += OnDirectionChanged;
        InputController.Instance.buttonAny += OnButtonDown;
        selectionIndex = 1;
        currentSelectedUI = StartButton;
        menuButtons = new List<MenuButton> { QuitButton, StartButton };
        currentSelectedUI.OnSelected();
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
        if (!selectionChangeable) return;

        if (currentSelectedUI == VolumeSlider)
        {
            if (dir.x > Mathf.Abs(dir.y) && dir.x > 0.5f)
            {
                // switch to one of the buttons
                SelectButton();
            }
            else
            {
                VolumeSlider.SlideChanged(dir);
            }
            return;
        }
        if (dir.x < -Mathf.Abs(dir.y) && dir.x < -0.5f) 
        {
            currentSelectedUI.OnDeselected();
            currentSelectedUI = VolumeSlider;
            currentSelectedUI.OnSelected();
            return;
        }

        int prevSelectionIndex = selectionIndex;
        if (dir.y > 0.5f)
        {
            selectionIndex = Mathf.Clamp(selectionIndex + 1, 0, menuButtons.Count - 1);
        }
        else if (dir.y < -0.5f)
        {
            selectionIndex = Mathf.Clamp(selectionIndex - 1, 0, menuButtons.Count - 1);
        }

        if (prevSelectionIndex != selectionIndex)
        {
            SelectButton();
        }
    }
    private void OnButtonDown(bool isDown) 
    {
        if (!isDown) return;
        if (currentSelectedUI == VolumeSlider)
        {
            return;
        }

        menuButtons[selectionIndex].OnClick();
        switch (selectionIndex)
        {
            case 0:
                QuitGame();
                break;
            case 1:
                StartGame();
                break;
        }
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
