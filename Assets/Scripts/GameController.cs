public class GameController : Controller<GameController> 
{
    protected override void MyStart()
    {
        DontDestroyOnLoad(this);
        LeaderboardService.LoadLeaderboard();
    }

    public void OnAttack()
    {
        BombSpawner.Instance.SpawnBomb();
    }
}