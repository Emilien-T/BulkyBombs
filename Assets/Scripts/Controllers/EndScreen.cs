using AudioSystem;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Sprite launchNukesSprite;
    [SerializeField] private Sprite badNukesSprite;
    public LeaderboardScene ldboardnameinput;

    private static LossReason reason;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    private void OnEnable()
    {
        Image image = GetComponent<Image>();
        switch (reason)
        {
            case LossReason.LaunchNukes:
                image.sprite = launchNukesSprite;
                break;
            case LossReason.BadNukes:
                image.sprite = badNukesSprite;
                break;
            default:
                break;
        }
        SoundManager.Instance.StopAllSounds();
        AudioLibrary.Instance.NuclearExplosion();
        DOVirtual.DelayedCall(3f, () => {
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
