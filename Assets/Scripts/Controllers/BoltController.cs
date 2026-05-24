using DG.Tweening;
using Enums;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    [SerializeField] private Color boltColor1;
    [SerializeField] private Color boltColor2;
    [SerializeField] private Color boltColor3;
    private BoltType boltType;
    private int boltingTimes = 0;
    private bool isFocused = false;
    private Transform originalTranform;

    private void Start()
    {
        originalTranform = transform;
    }


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

    public void Focus()
    {
        transform.DOScale(0.6f, 0.1f).SetEase(Ease.InOutQuad);
    }
    public void UnFocus()
    {
        transform.DOScale(0.5f, 0.1f).SetEase(Ease.InOutQuad);
    }

    public bool Bolt()
    {
        if (boltingTimes > (int)boltType)
        {
            // Bolt too many times, lose game
            return true;
        }
        boltingTimes++;
        transform.rotation = originalTranform.rotation;
        transform.DORotate(new Vector3(0f, 60f, 0f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
        return false;
    }

    public bool IsBolted()
    {
        return boltingTimes == (int)boltType+1;
    }
}
