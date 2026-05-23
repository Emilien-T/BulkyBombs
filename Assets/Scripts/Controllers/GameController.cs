public class GameController : Controller<GameController> 
{
    protected override void MyAwake()
    {
        DontDestroyOnLoad(this);
        LeaderboardService.LoadLeaderboard();
    }
}