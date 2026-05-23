using UnityEngine;

public class Nail : MonoBehaviour
{
    public Animator animator;
    public bool isNailed;
    public NailHammerUI nailingUI;
    public void Start() 
    {
        animator = GetComponent<Animator>();
    }
    public void StartNail() 
    {
        // have hand ready to hammer
        nailingUI.Enable();
        Debug.Log("Nailing " + this.gameObject);
    }
    public void SucceedNail()
    {
        Debug.Log("Nailing " + this.gameObject + " success");
        isNailed = true;
    }
    public void FailNail()
    {
        Debug.Log("Nailing " + this.gameObject + " failure");
    }
    public void CancelNail() 
    {
        // stop having hand ready to hammer
    }
}
