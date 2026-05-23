using Enums;
using System.Collections.Generic;
using UnityEngine;

public class BoltMinigame : Minigame
{
    [SerializeField] private BombController bombController;
    [SerializeField] private List<BoltController> boltList;
    private BoltType[] bolts = new BoltType[3];
    public void SetBolts(BoltType[] bolts)
    {
        this.bolts = bolts;
        for (int i = 0; i < boltList.Count; i++)
        {
            boltList[i].SetBoltType(bolts[i]);
        }
    }
     public override void StartGame() { }
     public override void OnSelect()
     {
         isFocused = true;
     }
     public override void OnDeselect()
     {
         isFocused = false;
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
