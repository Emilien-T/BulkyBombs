using DG.Tweening;
using Enums;
using System.Collections.Generic;
using UnityEngine;

public class BoltMinigame : Minigame
{
    [SerializeField] private BombController bombController;
    [SerializeField] private List<BoltController> boltList;
    [SerializeField] private GameObject Wrench;
    [SerializeField] private List<Transform> WrenchPositions;
    private bool boltingState = false;
    private int boltIndex = 0;
    private bool moveBoltFlag = false;
    private BoltType[] bolts = new BoltType[3];
    public void Start()
    {
        Wrench.SetActive(false);
    }
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
        Wrench.SetActive(true);
    }

    private void GoToWrenchPosition(int index)
    {
        if (index < 0 || index >= WrenchPositions.Count)
        {
            Debug.LogError("Invalid wrench position index: " + index);
            return;
        }
        Wrench.transform.DOMove(WrenchPositions[index].position, 0.2f).SetEase(Ease.InOutQuad);
        Wrench.transform.DORotateQuaternion(WrenchPositions[index].rotation, 0.2f).SetEase(Ease.InOutQuad);
    }

    private void BoltControl(Vector2 move)
    {
        if(bombController.currentMinigame != MinigameType.Bolt) 
        {
            return;
        }
        if (move.x > 0.8f && !boltingState)
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
            else
            {
                Wrench.transform.DORotate(new Vector3(-60f, 0, 0f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
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
            if (boltingState)
            {
                Wrench.transform.DORotate(new Vector3(60f, 0f, 0f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
            }
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
            GoToWrenchPosition(boltIndex);
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
            GoToWrenchPosition(boltIndex);
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
        Wrench.SetActive(false);
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
