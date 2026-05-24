using UnityEngine;

public class Nail : MonoBehaviour
{
    public Animator animator;
    public bool isNailed;
    public NailHammerUI nailingUI;
    public GameObject firstNail;
    public GameObject bentNail;
    public void StartNail() 
    {
        // have hand ready to hammer
        gameObject.SetActive(true);
        firstNail.SetActive(true);
        bentNail.SetActive(false);
        nailingUI.gameObject.SetActive(true);
        nailingUI.Enable();
        Debug.Log("Nailing " + this.gameObject);
    }
    public void SucceedNail()
    {
        Debug.Log("Nailing " + this.gameObject + " success");
        animator.SetTrigger("Succeed");
        isNailed = true;
    }
    public void FailNail()
    {
        Debug.Log("Nailing " + this.gameObject + " failure");
        animator.SetTrigger("Fail");
    }
    public void CancelNail()
    {
        gameObject.SetActive(false);
        firstNail.SetActive(false);
        bentNail.SetActive(false);
        nailingUI.gameObject.SetActive(false);
        nailingUI.Disable();
    }
}
