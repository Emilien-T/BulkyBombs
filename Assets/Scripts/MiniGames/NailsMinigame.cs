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
    private int currentNailIndex = 0;

    public Coroutine nailingCoroutine;
    public float successNailTime;
    public float failNailTime;
    public bool timerPaused;
    private void Start()
    {
        InputController.Instance.button0 += Hammer;
        foreach (var nail in nails) 
        {
            nail.nailingUI.minigameManager = this;
        }
    }
    private void OnDisable()
    {
        InputController.Instance.button0 -= Hammer;
    }
    private void Hammer(bool val) 
    {
        if (nailingCoroutine != null || completed || !isFocused || !val)
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
        isFocused = true;
        timer = 0;
        if (timerCoroutine != null) 
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        timerCoroutine = StartCoroutine(timerRoutine());
        nails[currentNailIndex].StartNail();
    }
    public override void OnDeselect()
    {
        isFocused = false;

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
        Debug.Log("lost nail game");
    }
}
