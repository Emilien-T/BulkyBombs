using DG.Tweening;
using UnityEngine;

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
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;

    public void TransitionToMinigame(Enums.MinigameType minigameType, Minigame minigame)
    {
        switch (minigameType)
        {
            case Enums.MinigameType.Button:
                GoToButton(minigame);
                break;
            case Enums.MinigameType.Bolt:
                GoToBolt(minigame);
                break;
            case Enums.MinigameType.Nails:
                GoToNail(minigame);
                break;
            case Enums.MinigameType.Zen:
                GoToZen();
                break;
            default:
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
        GoToZen(1f);
        GoToBase(4.8f);
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

    private void Transition(Transform target, Minigame targetMinigame, float startDelay = 0f)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(startDelay);
        seq.Append(transform.DOMove(target.position, transitionTime).SetEase(Ease.InOutQuad));
        seq.Join(transform.DORotate(target.rotation.eulerAngles, transitionTime).SetEase(Ease.InOutQuad));
        if(targetMinigame != null) seq.onComplete += () => targetMinigame.OnSelect();
    }

    public void GoToZen(float startDelay = 0f)
    {
        Transition(zenTransform, null, startDelay);
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
