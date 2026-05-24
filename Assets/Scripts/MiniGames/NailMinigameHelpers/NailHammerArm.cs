using UnityEngine;

public class NailHammerArm : MonoBehaviour
{
    public GameObject firstNail;
    public GameObject bentNail;
    public GameObject straightNail;

    public void SucceedHammer() 
    {
        firstNail.SetActive(false);
        straightNail.SetActive(true);
    }

    public void FailHammer() 
    {
        firstNail.SetActive(false);
        bentNail.SetActive(true);
    }
}
