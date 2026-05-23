using UnityEngine;
using Enums;

public class BoltController : MonoBehaviour
{
    [SerializeField] private Color boltColor1;
    [SerializeField] private Color boltColor2;
    [SerializeField] private Color boltColor3;
    private BoltType boltType;
    private int boltingTimes = 0;
    private bool isFocused = false;


    public void SetBoltType(BoltType boltType)
    {
        this.boltType = boltType;
        switch (boltType)
        {
            case BoltType.One:
                GetComponent<SpriteRenderer>().color = boltColor1;
                break;
            case BoltType.Two:
                GetComponent<SpriteRenderer>().color = boltColor2;
                break;
            case BoltType.Three:
                GetComponent<SpriteRenderer>().color = boltColor3;
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.white;
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
        return false;
    }

    public bool IsBolted()
    {
        return boltingTimes == (int)boltType+1;
    }
}
