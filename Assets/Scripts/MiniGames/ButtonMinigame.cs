using Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonMinigame : Minigame
{
    [SerializeField] public BombController bombController;
    public int gunkLeft = 0;
    private Brush brush;
    public int GoalPixelsLeft = 28000;
    public GameObject Triangle;
    public GameObject Circle;
    public GameObject Star;
    public GameObject Umbrella;
    public bool lost;

    private void Start()
    {
        brush = GetComponentInChildren<Brush>();
        brush.minigameManager = this;
        brush.pixelsLeft += OnCleanPixelsLeft;
    }
    private void OnCleanPixelsLeft(int pixelsLeft) 
    {
        if (pixelsLeft <= GoalPixelsLeft) 
        {
            brush.AutoCleanup();
            WinGame();
        }
    }
    public override void StartGame() 
    {

    }
    public void Setup(ButtonType buttonType) 
    {
        Vector3 eulerAnglesRot = new Vector3(0, Random.Range(0, 360f),0);
        switch (buttonType) 
        {
            case ButtonType.Triangle:
                Triangle.SetActive(true);
                Triangle.transform.localEulerAngles = eulerAnglesRot;
                break;
            case ButtonType.Circle:
                Circle.SetActive(true);
                Circle.transform.localEulerAngles = eulerAnglesRot;
                break;
            case ButtonType.Star:
                Star.SetActive(true);
                Star.transform.localEulerAngles = eulerAnglesRot;
                break;
            case ButtonType.Umbrella:
                Umbrella.SetActive(true);
                Umbrella.transform.localEulerAngles = eulerAnglesRot;
                break;
        }
        Debug.Log(buttonType);
    }
    private void OnDisable()
    {
        brush.pixelsLeft -= OnCleanPixelsLeft;
    }
    public override void OnSelect()
    {
        CameraController.Instance.rightHandTransform.gameObject.SetActive(false);
        CameraController.Instance.leftHandTransform.gameObject.SetActive(false);
        brush.animator.SetTrigger("Up");
    }

    public override void OnDeselect()
    {
        CameraController.Instance.rightHandTransform.gameObject.SetActive(true);
        CameraController.Instance.leftHandTransform.gameObject.SetActive(true);
        brush.animator.SetTrigger("Down");
    }
    public override void ForceDeselect()
    {
        OnDeselect();
    }
    public override void WinGame()
    {
        Debug.Log("Yay");
        completed = true;
        bombController.NextMiniGame();
    }

    public override void LoseGame()
    {
        brush.animator.SetTrigger("Down");
        lost = true;
        bombController.SendNukes();
    }
}
