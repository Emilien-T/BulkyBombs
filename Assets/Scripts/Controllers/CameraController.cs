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

    private void Transition(Transform target)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(target.position, transitionTime).SetEase(Ease.InOutQuad));
        seq.Join(transform.DORotate(target.rotation.eulerAngles, transitionTime).SetEase(Ease.InOutQuad));
    }

    public void GoToZen()
    {
        Transition(zenTransform);
    }

    public void GoToBase()
    {
        Transition(baseTransform);
    }

    public void GoToBolt()
    {
        Transition(boltTransform);
    }

    public void GoToButton()
    {
        Transition(buttonTransform);
    }

    public void GoToNail()
    {
        Transition(nailTransform);
    }
}
