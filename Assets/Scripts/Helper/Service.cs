/// <summary>
/// A Service must has name end with the word "Service".<br/>
/// A Service is a Singleton class that does NOT inherit from Godot's Node class.<br/>
/// This is for system that does not use any of the Godot Node functionality.<br/>
/// A good example of this will be the resource loading system.<br/>
/// If constructor is needed, use Initialize(T instance) method to initialize the instance, and make the constructor private. <br/>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Service<T>  where T : Service<T>, new()
{
    public static T Instance { get; private set; }

    public static void Initialize()
    {
        Instance = new T();
    }
    
    public static void Initialize(T instance)
    {
        Instance = instance;
    }
    
    public static void Free()
    {
        Instance = null;
    }
}