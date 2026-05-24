using DG.Tweening;
using DG.Tweening.Core.Easing;
using Enums;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Sprite launchNukesSprite;
    [SerializeField] private Sprite badNukesSprite;
    [SerializeField] private GameObject allowContinueUI;
    private bool allowContinuing = false;

    private static LossReason reason;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Continue(bool action)
    {
        if (allowContinuing)
        {
            SceneController.Instance.LoadScene("MainMenu");
        }
    }

    private void OnEnable()
    {
        switch (reason)
        {
            case LossReason.LaunchNukes:

                break;
            case LossReason.BadNukes:
                break;
            default:
                break;
        }
        allowContinuing = false;
        allowContinueUI.SetActive(false);
        InputController.Instance.buttonAny += Continue;
        DOVirtual.DelayedCall(3f, () => {
            allowContinuing = true;
            allowContinueUI.SetActive(true);
        });
    }

    private void OnDisable()
    {
        InputController.Instance.buttonAny -= Continue;
    }

    public static void SetLossReason(LossReason r)
    {
        reason = r;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
