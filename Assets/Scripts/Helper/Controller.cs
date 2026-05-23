using UnityEngine;


/// <summary>
/// A Controller must has name end with the word "Controller".<br/>
/// A Controller is a Singleton class that inherits from Godot's Node class.<br/>
/// A Controller can only be used in scenes, as nodes.<br/>
/// A good example of this will be any UI Manager.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Controller<T> : MonoBehaviour where T : Controller<T>, new()
{
    public static T Instance { get; private set; }

    public void Start()
    {
        if (Instance != null)
        {
            print($"An instance of {typeof(T).Name} already exists. Destroying the new one.");
            Destroy(Instance);
            return;
        }

        Instance = (T)this;
        MyStart();
    }
    protected virtual void MyStart() { }

    public void OnDestory()
    {
        if (Instance == this)
        {
            Instance = null;
        }
        else
        {
            print($"Instance of {typeof(T).Name} is not this node. This should not happen.");
        }
    }
}