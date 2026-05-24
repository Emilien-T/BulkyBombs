using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NailsMinigame : Minigame
{
    [SerializeField] private BombController bombController;
    public float timer;
    public float timerMax;
    public float timerCorrectMinPercentage;
    public float timerCorrectMaxPercentage;
    Coroutine timerCoroutine;
    public List<Nail> nails = new();
    public int currentNailIndex = 0;

    public Coroutine nailingCoroutine;
    public float successNailTime;
    public float failNailTime;
    public bool timerPaused;
    public Animator lidAnimator;
    private bool raging = false;
    private void Start()
    {
        InputController.Instance.button1 += Hammer;
        foreach (var nail in nails) 
        {
            nail.nailingUI.minigameManager = this;
        }
    }
    private void OnDisable()
    {
        InputController.Instance.button1 -= Hammer;
    }
    private void Hammer(bool val) 
    {
        if (nailingCoroutine != null || completed || bombController.currentMinigame != Enums.MinigameType.Nails || !val)
        {
            return;
        }
        timerPaused = true;
        nails[currentNailIndex].nailingUI.Hammer();
        if (timer <= timerCorrectMaxPercentage * timerMax && timer >= timerCorrectMinPercentage * timerMax)
        {
            nails[currentNailIndex].SucceedNail();
            nailingCoroutine = StartCoroutine(SuccessNailRoutine());
        }
        else 
        {
            nails[currentNailIndex].FailNail();
            nailingCoroutine = StartCoroutine(FailNailRoutine());
        }
    }
    public override void OnSelect()
    {
        raging = false;
        timer = 0;
        if (timerCoroutine != null) 
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        timerCoroutine = StartCoroutine(timerRoutine());
        nails[currentNailIndex].StartNail();
        lidAnimator.SetTrigger("Close");
        AudioLibrary.Instance.BoxClosing();
        // disable hand cuz we want our own
        CameraController.Instance.rightHandTransform.gameObject.SetActive(false);
    }
    public override void OnDeselect()
    {
        // reenable hand cuz we want our own
        CameraController.Instance.rightHandTransform.gameObject.SetActive(true);
        nails[currentNailIndex].CancelNail();
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        if (nailingCoroutine != null) 
        {
            StopCoroutine(nailingCoroutine);
            nailingCoroutine = null;
        }
        if (currentNailIndex == 0 && !raging) 
        {

            lidAnimator.SetTrigger("Open");
        }
    }
    public override void ForceDeselect() 
    {
        OnDeselect();
    }
    public IEnumerator timerRoutine() 
    {
        while (!completed) 
        {
            yield return null;
            if(!timerPaused) timer += Time.deltaTime;
            if(timer > timerMax) timer = 0;
        }
    }
    private IEnumerator SuccessNailRoutine() 
    {
        yield return new WaitForSeconds(successNailTime);

        nailingCoroutine = null;
        timer = 0;
        nails[currentNailIndex].nailingUI.Disable();
        nails[currentNailIndex].nailingUI.gameObject.SetActive(false);
        if (nails.Count > currentNailIndex + 1)
        {
            currentNailIndex++;
            nails[currentNailIndex].StartNail();
        }
        else 
        {
            WinGame();
        }
        timerPaused = false;
    }
    public override void WinGame()
    {
        Debug.Log("won nail game");
        completed = true;
        bombController.TransitionOut();
    }
    private IEnumerator FailNailRoutine() 
    {
        yield return new WaitForSeconds(failNailTime);
        nails[currentNailIndex].nailingUI.Disable();
        nailingCoroutine = null;

        timerPaused = false;
        LoseGame();
    }
    public override void LoseGame()
    {
        raging = true;
        ForceDeselect();
        bombController.Rage();
    }
}
