using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class TimerController : Controller<TimerController>
{
    [SerializeField] private TextMeshProUGUI text;
    private float TimeRemaining = 0f;
    private BombController currentBombController;

    public void StartTimer(float time, BombController bombController)
    {
        TimeRemaining = time;
        currentBombController = bombController;
    }

    private void Update()
    {
        TimeSpan t = TimeSpan.FromSeconds(TimeRemaining);
        string formatted = string.Format("{0:00}:{1:00}", (int)t.TotalSeconds, t.Milliseconds / 10);
        text.text = string.Concat(formatted.Select(c => c == '1' ? "<mspace=0.5em>1</mspace>" : c.ToString()));
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            if(TimeRemaining < 0)
            {
                TimeRemaining = 0;
                currentBombController.TransitionOut();
            }
        }
        else
        {
            TimeRemaining = 0;
        }

    }


}
