using AudioSystem;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraController : Controller<CameraController>
{
    [SerializeField] private Transform zenTransform;
    [SerializeField] private Transform baseTransform;
    [SerializeField] private Transform boltTransform;
    [SerializeField] private Transform buttonTransform;
    [SerializeField] private Transform nailTransform;
    [SerializeField] private float transitionTime = 0.7f;
    [SerializeField] private Animator leftHandAnimator;
    [SerializeField] private Animator rightHandAnimator;
    [SerializeField] public Transform leftHandTransform;
    [SerializeField] public Transform rightHandTransform;
    public Material rageMat;
    private SoundBuilder sweepSound;
    protected override void MyAwake() 
    {

        rageMat.SetFloat("_vignette_power", 10);
    }
    public void TransitionToMinigame(Enums.MinigameType minigameType, Minigame minigame)
    {
        sweepSound?.Stop();
        switch (minigameType)
        {
            case Enums.MinigameType.Button:
                sweepSound = AudioLibrary.Instance.SweepToWork();
                GoToButton(minigame);
                break;
            case Enums.MinigameType.Bolt:
                sweepSound = AudioLibrary.Instance.SweepToWork();
                GoToBolt(minigame);
                break;
            case Enums.MinigameType.Nails:
                sweepSound = AudioLibrary.Instance.SweepToWork();
                GoToNail(minigame);
                break;
            case Enums.MinigameType.Zen:
                sweepSound = AudioLibrary.Instance.SweepToZen();
                GoToZen();
                break;
            default:
                sweepSound = AudioLibrary.Instance.SweepToWork();
                GoToBase();
                break;
        }
    }

    public void Rage()
    {
        rightHandAnimator.speed = 1.5f;
        leftHandAnimator.speed = 1.5f;
        rightHandAnimator.SetTrigger("Zen");
        leftHandAnimator.SetTrigger("Zen");
        // 7 -> 3 during delay before zen
        DOVirtual.Float(10f, 3f, 1f, value =>
        {
            rageMat.SetFloat("_vignette_power", value);
        });

        GoToZen(1f);
        GoToBase(4.8f);

        // 3 -> 7 ending at end of rage
        DOVirtual.Float(3f, 10f, 4f, value =>
        {
            rageMat.SetFloat("_vignette_power", value);
        })
        .SetDelay(1f);

        rightHandTransform.DOShakePosition(4f, strength: 0.1f, vibrato: 20, randomness: 0).SetEase(Ease.OutQuad);
        leftHandTransform.DOShakePosition(4f, strength: 0.1f, vibrato: 20, randomness: 0).SetEase(Ease.OutQuad);
        DOVirtual.DelayedCall(5f, () =>
        {
            rightHandAnimator.SetTrigger("Idle");
            leftHandAnimator.SetTrigger("Idle");
        });
    }

    public void CantStartYet()
    {
        transform.DOShakePosition(0.2f, strength: 0.1f, vibrato: 40, randomness: 0).SetEase(Ease.OutQuad);
    }

    private void Transition(Transform target, Minigame targetMinigame, float startDelay = 0f, bool zenAmbience = false)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(startDelay);
        if(zenAmbience)
            seq.AppendCallback(() =>
                {
                    AudioLibrary.Instance.StartZenAmbience();
                });
        seq.Append(transform.DOMove(target.position, transitionTime).SetEase(Ease.InOutQuad));
        seq.Join(transform.DORotate(target.rotation.eulerAngles, transitionTime).SetEase(Ease.InOutQuad));
        if(targetMinigame != null) targetMinigame.OnSelect();
    }

    public void GoToZen(float startDelay = 0f)
    {
        Transition(zenTransform, null, startDelay, true);
    }

    public void GoToBase(float startDelay = 0f)
    {
        Transition(baseTransform, null, startDelay);
    }

    public void GoToBolt(Minigame boltMinigame)
    {
        Transition(boltTransform, boltMinigame);
    }

    public void GoToButton(Minigame buttonMinigame)
    {
        Transition(buttonTransform, buttonMinigame);
    }

    public void GoToNail(Minigame nailMinigame)
    {
        Transition(nailTransform, nailMinigame);
    }
}
