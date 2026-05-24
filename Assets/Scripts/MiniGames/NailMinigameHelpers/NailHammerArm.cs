using UnityEngine;

public class NailHammerArm : MonoBehaviour
{
    public GameObject firstNail;
    public GameObject bentNail;
    public GameObject straightNail;

    public void SucceedHammer()
    {
        AudioLibrary.Instance.Hammering();
        firstNail.SetActive(false);
        straightNail.SetActive(true);
    }

    public void FailHammer()
    {
        AudioLibrary.Instance.Hammering();
        firstNail.SetActive(false);
        bentNail.SetActive(true);
    }
}
