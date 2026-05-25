using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScene : MonoBehaviour
{
    public List<Letter> letters = new();
    public int currentLetterIndex;
    public bool setup;
    public GameObject blackout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputController.Instance.button0 += Confirm;
        InputController.Instance.directionalControlsDown += Move;
    }
    public void Setup() 
    {
        setup = true;
        blackout.SetActive(true);
        this.gameObject.SetActive(true);
        currentLetterIndex = 0;
        letters[0].Activate();
    }
    private void OnDisable()
    {

        InputController.Instance.button0 -= Confirm;
        InputController.Instance.directionalControlsDown -= Move;
    }
    private void Confirm(bool val) 
    {
        if (!setup) return;
        if (!val) return;

        if (currentLetterIndex == letters.Count-1)
        {
            string name = "";
            foreach (Letter l in letters) 
            {
                name += l.text.text;
            }
            LeaderboardService.SaveToLeaderboard(new LeaderboardEntry(name, GameController.Instance.score));
            SceneController.Instance.LoadScene("MainMenu");
        }
        else
        {
            letters[currentLetterIndex].Deactivate();
            currentLetterIndex = Mathf.Clamp(currentLetterIndex + 1, 0, letters.Count-1);
            letters[currentLetterIndex].Activate();
        }
    }
    private void Move(Vector2 dir)
    {

        if (!setup) return;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                letters[currentLetterIndex].Deactivate();
                currentLetterIndex = Mathf.Clamp(currentLetterIndex + 1, 0, letters.Count-1);
                letters[currentLetterIndex].Activate();
            }
            else
            {
                letters[currentLetterIndex].Deactivate();
                currentLetterIndex = Mathf.Clamp(currentLetterIndex - 1, 0, letters.Count-1);
                letters[currentLetterIndex].Activate();
            }
        }
        else 
        {
            if (dir.y > 0)
            {
                letters[currentLetterIndex].LetterUp();
            }
            else
            {
                letters[currentLetterIndex].LetterDown();
            }
        }
    }
}
