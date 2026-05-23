using UnityEngine.SceneManagement;

public class SceneController : Controller<SceneController> 
{
    protected override void MyAwake()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadScene(string sceneToLoad) 
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}