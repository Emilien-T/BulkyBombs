using UnityEngine;

public class GameController : Controller<GameController>
{
    public GameObject leftHand;
    public GameObject rightHand;
    protected override void MyAwake()
    {
        DontDestroyOnLoad(this);
        LeaderboardService.LoadLeaderboard();
    }

    private void Start()
    {
        InputController.Instance.button0 += NextTask;
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