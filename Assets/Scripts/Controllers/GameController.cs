public class GameController : Controller<GameController> 
{
    protected override void MyAwake()
    {
        DontDestroyOnLoad(this);
        LeaderboardService.LoadLeaderboard();
    }

    public void OnAttack()
    {
        BombSpawner.Instance.SpawnBomb();
    }

    public void OnButton1()
    {
        CameraController.Instance.GoToBase();
    }

    public void OnButton2()
    {
        CameraController.Instance.GoToBolt();
    }

    public void OnButton3()
    {
        CameraController.Instance.GoToButton();
    }

    public void OnButton0()
    {
        CameraController.Instance.GoToZen();
    }

}