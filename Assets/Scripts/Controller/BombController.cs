using AudioSystem;
using DG.Tweening;
using Enums;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] public float bombTimer = 2f;
    [SerializeField] private Vector3 workPosition;
    [SerializeField] private Vector3 transitionOut;
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private BoltType[] bolts = new BoltType[3];
    [SerializeField] private BoltMinigame boltMinigame;
    [SerializeField] private ButtonMinigame buttonMinigame;
    [SerializeField] private NailsMinigame nailsMinigame;
    public GameObject nextTaskUI;
    public GameObject InteractUI;
    public GameObject polishUI;
    public GameObject tightenAndUI;
    public MinigameType currentMinigame = MinigameType.None;
    private int currentMinigameIndex = 0;
    private bool transitioning = false;

    private MinigameType tempMinigame;

    private Tween _moveTween;
    private bool activeBomb = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transitioning = false;
        buttonType = Random.Range(0, 4) switch
        {
            0 => ButtonType.Circle,
            1 => ButtonType.Star,
            2 => ButtonType.Triangle,
            _ => ButtonType.Umbrella
        };
        buttonMinigame.Setup(buttonType);

        for (int i = 0; i < bolts.Length; i++)
        {
            bolts[i] = Random.Range(0, 3) switch
            {
                0 => BoltType.One,
                1 => BoltType.Two,
                _ => BoltType.Three
            };
        }
        boltMinigame.SetBolts(bolts);
        StartConveyor();
    }

    public void NextMiniGame()
    {
        Minigame chosenMinigame = null;
        switch (currentMinigame)
        {
            case MinigameType.None:
                chosenMinigame = buttonMinigame;
                currentMinigame = MinigameType.Button;
                polishUI.SetActive(true);
                nextTaskUI.SetActive(true);
                tightenAndUI.SetActive(false);
                InteractUI.SetActive(true);
                break;
            case MinigameType.Button:
                buttonMinigame.OnDeselect();
                chosenMinigame = boltMinigame;
                currentMinigame = MinigameType.Bolt;
                polishUI.SetActive(false);
                nextTaskUI.SetActive(true);
                tightenAndUI.SetActive(true);
                InteractUI.SetActive(false);
                break;
            case MinigameType.Bolt:
                boltMinigame.OnDeselect();
                chosenMinigame = nailsMinigame;
                currentMinigame = MinigameType.Nails;
                polishUI.SetActive(false);
                nextTaskUI.SetActive(true);
                tightenAndUI.SetActive(false);
                InteractUI.SetActive(true);
                break;
            case MinigameType.Nails:
                nailsMinigame.OnDeselect();
                currentMinigame = MinigameType.None;
                polishUI.SetActive(false);
                nextTaskUI.SetActive(true);
                tightenAndUI.SetActive(false);
                InteractUI.SetActive(false);
                break;
            case MinigameType.Zen:
                polishUI.SetActive(false);
                nextTaskUI.SetActive(false);
                tightenAndUI.SetActive(false);
                InteractUI.SetActive(false);
                break;
            default:
                break;
        }

        if (currentMinigame == MinigameType.Button && (buttonMinigame.completed || nailsMinigame.currentNailIndex != 0))
        {
            NextMiniGame();
        }
        else if (currentMinigame == MinigameType.Bolt && (boltMinigame.completed || nailsMinigame.currentNailIndex != 0))
        {
            NextMiniGame();
        }
        else if (currentMinigame == MinigameType.Nails && nailsMinigame.completed) 
        {
            NextMiniGame();
        }
        else 
        {
            CameraController.Instance.TransitionToMinigame(currentMinigame, chosenMinigame);
        }
    }

    private Minigame GetCurrentMinigame()
    {
        return currentMinigame switch
        {
            MinigameType.Button => buttonMinigame,
            MinigameType.Bolt => boltMinigame,
            MinigameType.Nails => nailsMinigame,
            _ => null
        };
    }

    private void StartConveyor()
    {
        SoundBuilder conveyor = AudioLibrary.Instance.Conveyor();
        _moveTween?.Kill();
        _moveTween = transform.DOMove(workPosition, 2f).SetEase(Ease.Linear).OnComplete(()=>
        {
            TimerController.Instance.StartTimer(bombTimer, this);
            activeBomb = true;
            conveyor.Stop();
        });
    }

    public void TransitionOut()
    {
        if (transitioning) return;
        transitioning = true;
        switch (currentMinigame) 
        {
            case MinigameType.Bolt:
                break;
            case MinigameType.Nails:
                nailsMinigame.ForceDeselect();
                break;
            case MinigameType.Button:
                buttonMinigame.ForceDeselect();
                break;
        }
        polishUI.SetActive(false);
        nextTaskUI.SetActive(true);
        tightenAndUI.SetActive(false);
        InteractUI.SetActive(false);
        currentMinigame = MinigameType.None;
        CameraController.Instance.TransitionToMinigame(currentMinigame, null);
        _moveTween?.Kill();
        AudioLibrary.Instance.BombGoingOut();
        _moveTween = transform.DOMove(transitionOut, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            tempMinigame = MinigameType.None;
            if (CheckBombCompleted())
            {
                BombSpawner.Instance.score++;
                BombSpawner.Instance.SpawnBomb();
                Destroy(gameObject);
            }
            else
            {
                EndScreen.SetLossReason(LossReason.BadNukes);
                GameController.Instance.score = BombSpawner.Instance.score;
                SceneController.Instance.LoadScene("EndScreen");
            }
        });
    }

    public bool CheckBombCompleted()
    {
        return buttonMinigame.completed && boltMinigame.completed && nailsMinigame.completed;
    }

    public bool IsActiveBomb()
    {
        return activeBomb;
    }

    public void SendNukes()
    {
        EndScreen.SetLossReason(LossReason.LaunchNukes);
        Debug.Log("Sending nukes...");
        SceneController.Instance.LoadScene("EndScreen");
    }

    public void Rage()
    {
        tempMinigame = currentMinigame;
        currentMinigame = MinigameType.Zen;
        polishUI.SetActive(false);
        nextTaskUI.SetActive(false);
        tightenAndUI.SetActive(false);
        InteractUI.SetActive(false);
        CameraController.Instance.Rage();
        //AudioLibrary.Instance.GettingMad();
        DOVirtual.DelayedCall(5f, () =>
        {
            currentMinigame = tempMinigame;
            switch (currentMinigame)
            {
                case MinigameType.None:
                    polishUI.SetActive(false);
                    nextTaskUI.SetActive(true);
                    tightenAndUI.SetActive(false);
                    InteractUI.SetActive(false);
                    break;
                case MinigameType.Button:
                    polishUI.SetActive(true);
                    nextTaskUI.SetActive(true);
                    tightenAndUI.SetActive(false);
                    InteractUI.SetActive(true);
                    break;
                case MinigameType.Bolt:
                    polishUI.SetActive(false);
                    nextTaskUI.SetActive(true);
                    tightenAndUI.SetActive(true);
                    InteractUI.SetActive(false);
                    break;
                case MinigameType.Nails:
                    polishUI.SetActive(false);
                    nextTaskUI.SetActive(true);
                    tightenAndUI.SetActive(false);
                    InteractUI.SetActive(true);
                    break;
                case MinigameType.Zen:
                    polishUI.SetActive(false);
                    nextTaskUI.SetActive(false);
                    tightenAndUI.SetActive(false);
                    InteractUI.SetActive(false);
                    break;
                default:
                    break;
            }
            AudioLibrary.Instance.CalmingDown();
            AudioLibrary.Instance.StartBombFactoryAmbience();
            CameraController.Instance.TransitionToMinigame(currentMinigame, GetCurrentMinigame());
            Debug.Log("Back to previous minigame: " + currentMinigame);
        });
        Debug.Log("Rage!!!!!!!!!!!");
    }

     // Update is called once per frame
    void Update()
    {
        if (activeBomb)
        {
            if (bombTimer < 6 && bombTimer % 1 - Time.deltaTime < 0 && currentMinigame != MinigameType.Zen) AudioLibrary.Instance.LowTime();
            else if (bombTimer > 6 && bombTimer % 1 - Time.deltaTime < 0 && currentMinigame != MinigameType.Zen) AudioLibrary.Instance.TimerBeep();
                bombTimer -= Time.deltaTime;
            if (bombTimer <= 0)
            {
                activeBomb = false;
                TransitionOut();
            }
        }
    }
}
