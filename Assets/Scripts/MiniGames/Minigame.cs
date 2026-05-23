using UnityEngine;

public class Minigame : MonoBehaviour
{
    public bool completed;
    public bool isFocused;
    public virtual void OnSelect() { }
    public virtual void OnDeselect() { }
    public virtual void ForceDeselect() { }
    public virtual void WinGame() { }
    public virtual void StartGame() { }
    public virtual void LoseGame() { }

}