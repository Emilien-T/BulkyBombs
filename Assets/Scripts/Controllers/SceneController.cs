using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Controller<SceneController> 
{
    protected override void MyAwake()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadScene(string sceneToLoad) 
    {
        Debug.Log("Trying to load scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}