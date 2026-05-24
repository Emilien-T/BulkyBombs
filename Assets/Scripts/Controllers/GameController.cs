using UnityEngine;

public class GameController : Controller<GameController>
{
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