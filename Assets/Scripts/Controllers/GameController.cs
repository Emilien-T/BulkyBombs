public class GameController : Controller<GameController> 
{
    protected override void MyAwake()
    {
        DontDestroyOnLoad(this);
        LeaderboardService.LoadLeaderboard();
    }

    private void Start()
    {
        InputController.Instance.buttonAny += NextTask;
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