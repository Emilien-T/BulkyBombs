using System.Collections;
using UnityEngine;

public class NailHammerUI : MonoBehaviour 
{
    public AnimationCurve animationCurve;
    public NailsMinigame minigameManager;
    public GameObject uiMovingNail;
    public GameObject BackgroundUI;
    public Vector2 uiMovingNailMoveDist;
    public Vector2 uiMovingNailHammerMoveDist;

    private Vector2 uiMovingNailStartPos;
    private Vector2 uiMovingNailEndPos;
    private RectTransform uiMovingNailRT;
    private bool isEnabled;
    private Coroutine displayTimerCoroutine;
    private bool timerStopped;
    public void Start()
    {
        uiMovingNailRT = uiMovingNail.GetComponent<RectTransform>();
        uiMovingNailStartPos = uiMovingNailRT.anchoredPosition;
        uiMovingNailEndPos = uiMovingNailStartPos + uiMovingNailMoveDist;
    }
    public void Hammer() 
    {
        timerStopped = true;
        uiMovingNailRT.anchoredPosition += uiMovingNailHammerMoveDist;
    }
    private IEnumerator timerRoutine() 
    {
        while (isEnabled && !timerStopped) 
        {
            float t = animationCurve.Evaluate(minigameManager.timer / minigameManager.timerMax);
            Vector2 correctPos = Vector2.Lerp(uiMovingNailStartPos, uiMovingNailEndPos, t);
            uiMovingNailRT.anchoredPosition = correctPos;
            yield return null;
        }
    }
    public void Enable()
    {
        uiMovingNail.SetActive(true);
        BackgroundUI.SetActive(true);
        isEnabled = true;
        if (displayTimerCoroutine != null) 
        {
            StopCoroutine(displayTimerCoroutine);
            displayTimerCoroutine = null;
        }
        displayTimerCoroutine = StartCoroutine(timerRoutine());
    }
    public void Disable()
    {
        uiMovingNail.SetActive(false);
        BackgroundUI.SetActive(false);
        isEnabled = false;

        if (displayTimerCoroutine != null)
        {
            StopCoroutine(displayTimerCoroutine);
            displayTimerCoroutine = null;
        }
    }
}