using UnityEngine;

public class GameController : Controller<GameController>
{
    public int score;
    protected override void MyAwake()
    {
        LeaderboardService.LoadLeaderboard();
    }

    private void Start()
    {
        InputController.Instance.button0 += NextTask;
        AudioLibrary.Instance.StartBombFactoryAmbience();
    }

    private void NextTask(bool startingPress)
    {
        if (BombSpawner.Instance == null) return;
        if (startingPress)
        {
            if(BombSpawner.Instance.GetCurrentBomb() == null || !BombSpawner.Instance.GetCurrentBomb().IsActiveBomb())
            {
                CameraController.Instance.CantStartYet();
            }
            else
            {
                BombSpawner.Instance.GetCurrentBomb().NextMiniGame();
            }
        }
    }
}