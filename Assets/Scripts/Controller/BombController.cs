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

    private Tween _moveTween;
    private bool activeBomb = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonType = Random.Range(0, 4) switch
        {
            0 => ButtonType.Circle,
            1 => ButtonType.Star,
            2 => ButtonType.Triangle,
            _ => ButtonType.Umbrella
        };

        for (int i = 0; i < bolts.Length; i++)
        {
            bolts[i] = Random.Range(0, 3) switch
            {
                0 => BoltType.One,
                1 => BoltType.Two,
                _ => BoltType.Three
            };
        }
        StartConveyor();
    }

    private void StartConveyor()
    {
        _moveTween?.Kill();
        _moveTween = transform.DOMove(workPosition, 2f).SetEase(Ease.Linear).OnComplete(()=>
        {
            TimerController.Instance.StartTimer(bombTimer, this);
            activeBomb = true;
        });
    }

    public void TransitionOut()
    {
        _moveTween?.Kill();
        _moveTween = transform.DOMove(transitionOut, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (CheckBombCompleted())
            {
                BombSpawner.Instance.SpawnBomb();
                Destroy(gameObject);
            }
            else
            {
                // For now, but this is the lose state. TODO: Add lose screen later.
                BombSpawner.Instance.SpawnBomb();
                Destroy(gameObject);
            }
        });
    }

    public bool CheckBombCompleted()
    {
        return buttonMinigame.completed && boltMinigame.completed && nailsMinigame.completed;
    }

     // Update is called once per frame
    void Update()
    {
        if (activeBomb)
        {
            bombTimer -= Time.deltaTime;
            if (bombTimer <= 0)
            {
                activeBomb = false;
                TransitionOut();
            }
        }
    }
}
