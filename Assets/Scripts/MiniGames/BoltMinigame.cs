using DG.Tweening;
using Enums;
using System.Collections.Generic;
using UnityEngine;

public class BoltMinigame : Minigame
{
    [SerializeField] private BombController bombController;
    [SerializeField] private List<BoltController> boltList;
    private bool boltingState = false;
    private int boltIndex = 0;
    private bool moveBoltFlag = false;
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
        boltIndex = 0;
    }
    public override void OnSelect()
    {
        boltIndex = 0;
        boltingState = false;
        InputController.Instance.directionalControls += BoltControl;
        boltList[boltIndex].Focus();
    }

    private void BoltControl(Vector2 move)
    {
        if(move.x > 0.8f && !boltingState)
        {
            boltingState = true;
            if (boltList[boltIndex].Bolt())
            {
                InputController.Instance.directionalControls -= BoltControl;
                bombController.Rage();
                DOVirtual.DelayedCall(5f, () =>
                {
                    InputController.Instance.directionalControls += BoltControl;
                });
                }
            bool tempCompleted = true;
            for (int i = 0; i < boltList.Count; i++)
            {
                tempCompleted &= boltList[i].IsBolted();
            }
            completed = tempCompleted;
            // Will auto next minigame if all bolts are bolted, but at the same time allow failure for spamming the bolt button
            if (completed)
            {
                DOVirtual.DelayedCall(0.2f, () =>
                {
                    if(bombController.currentMinigame == MinigameType.Bolt)
                    {
                        bombController.NextMiniGame();
                    }
                });
            }
        }
        else if (move.x < -0.8f)
        {
            boltingState = false;
        }
        else if (move.y > 0.8f && !moveBoltFlag)
        {
            moveBoltFlag = true;
            boltList[boltIndex].UnFocus();
            boltIndex--;
            if (boltIndex < 0)
            {
                boltIndex = bolts.Length - 1;
            }
            boltList[boltIndex].Focus();
        }
        else if (move.y < -0.8f && !moveBoltFlag)
        {
            moveBoltFlag = true;
            boltList[boltIndex].UnFocus();
            boltIndex++;
            if (boltIndex == bolts.Length)
            {
                boltIndex = 0;
            }
            boltList[boltIndex].Focus();
        }
        else if (Mathf.Abs(move.y) < 0.8f)
        {
            moveBoltFlag = false;
        }
    }

    public override void OnDeselect()
    {
        InputController.Instance.directionalControls -= BoltControl;
        boltList[boltIndex].UnFocus();
    }

    private void OnDestroy()
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
