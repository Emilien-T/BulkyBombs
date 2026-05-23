using UnityEngine;
using Enums;

public class BoltController : MonoBehaviour
{
    [SerializeField] private Color boltColor1;
    [SerializeField] private Color boltColor2;
    [SerializeField] private Color boltColor3;
    [SerializeField] private Animator animator;
    private BoltType boltType;
    private int boltingTimes = 0;
    private bool isFocused = false;


    public void SetBoltType(BoltType boltType)
    {
        Debug.Log("Setting bolt type to " + boltType);
        this.boltType = boltType;
        Renderer renderer = GetComponent<Renderer>();
        switch (boltType)
        {
            case BoltType.One:
                renderer.materials[0].color = boltColor1;
                break;
            case BoltType.Two:
                renderer.materials[0].color = boltColor2;
                break;
            case BoltType.Three:
                renderer.materials[0].color = boltColor3;
                break;
            default:
                renderer.materials[0].color = Color.white;
                break;
        }
    }

    public void FocusOnBolt()
    {
        isFocused = true;
    }

    public void UnfocusBolt()
    {
        isFocused = false;
    }

    public bool Bolt()
    {
        if (boltingTimes > (int)boltType)
        {
            // Bolt too many times, lose game
            return true;
        }
        boltingTimes++;
        animator.SetTrigger("Bolt");
        return false;
    }

    public bool IsBolted()
    {
        return boltingTimes == (int)boltType+1;
    }
}
