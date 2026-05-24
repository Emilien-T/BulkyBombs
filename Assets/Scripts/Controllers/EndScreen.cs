using DG.Tweening;
using DG.Tweening.Core.Easing;
using Enums;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Sprite launchNukesSprite;
    [SerializeField] private Sprite badNukesSprite;
    [SerializeField] private GameObject allowContinueUI;
    public LeaderboardScene ldboardnameinput;
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
        DOVirtual.DelayedCall(3f, () => {
            allowContinuing = true;
            allowContinueUI.SetActive(true);
            ldboardnameinput?.Setup();
        });
    }

    private void OnDisable()
    {
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
