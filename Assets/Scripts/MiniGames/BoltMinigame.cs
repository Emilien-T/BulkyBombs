using Enums;
using System.Collections.Generic;
using UnityEngine;

public class BoltMinigame : Minigame
{
    [SerializeField] private BombController bombController;
    [SerializeField] private List<BoltController> boltList;
    private bool boltingState = false;
    private int boltIndex = 0;
    private int boltingPush = 0;
    private BoltType[] bolts = new BoltType[3];
    public void SetBolts(BoltType[] bolts)
    {
        this.bolts = bolts;
        for (int i = 0; i < boltList.Count; i++)
        {
            boltList[i].SetBoltType(bolts[i]);
        }
    }

    public override void StartGame()
    {
        boltingPush = 0;
        boltIndex = 0;
    }
    public override void OnSelect()
    {
        boltingPush = 0;
        boltIndex = 0;
        boltingState = false;
        InputController.Instance.directionalControls += BoltControl;
    }

    private void BoltControl(Vector2 move)
    {
        if(move.x > 0.5f && !boltingState)
        {
            boltingState = true;
            if (boltList[boltIndex].Bolt())
            {
                bombController.Rage();
            }
        }
        else if (move.x < -0.5f)
        {
            boltingState = false;
        }
        if (move.y > 0.5f && boltList[boltIndex].IsBolted())
        {
            boltIndex++;
            if (boltIndex == bolts.Length)
            {
                boltIndex = 0;
            }
        }
        else if (move.y < -0.5f)
        {
            boltIndex--;
            if (boltIndex < 0)
            {
                boltIndex = bolts.Length - 1;
            }
        }
    }

    public override void OnDeselect()
    {
        InputController.Instance.directionalControls -= BoltControl;
    }
    public override void ForceDeselect()
    {
        OnDeselect();
    }
    public override void WinGame()
    {
        Debug.Log("Yay");
        completed = true;
    }
    public override void LoseGame()
    {
        Debug.Log("Lost");
    }
}
