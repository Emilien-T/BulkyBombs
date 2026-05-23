using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonMinigame : Minigame
{
    [SerializeField] private BombController bombController;
    public int gunkLeft = 0;
    private Brush brush;
    public int GoalPixelsLeft;

    private void Start()
    {
        brush = GetComponentInChildren<Brush>();
        brush.minigameManager = this;
        brush.pixelsLeft += OnCleanPixelsLeft;
    }
    private void OnCleanPixelsLeft(int pixelsLeft) 
    {
        if (pixelsLeft <= GoalPixelsLeft) 
        {
            WinGame();
        }
    }
    public override void StartGame() { }
    private void OnDisable()
    {
        brush.pixelsLeft -= OnCleanPixelsLeft;
    }
    public override void OnSelect()
    {
        isFocused = true;
    }

    public override void OnDeselect()
    {
        isFocused = false;
    }
    public override void ForceDeselect()
    {
        OnDeselect();
    }
    public override void WinGame()
    {
        Debug.Log("Yay");
        completed = true;
    }

    public override void LoseGame()
    {
        Debug.Log("Lost");
    }
}
