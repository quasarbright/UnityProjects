using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Text))]
public class Countdown : MonoBehaviour
{
    Text timerText;

    public UnityEvent OnFinish;

    public float duration = 60;// in seconds
    float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
        timeLeft = duration;
    }

    // Update is called once per frame
    void Update()
    {
        float newTimeLeft = timeLeft - Time.deltaTime;
        if (newTimeLeft >= 0)
        {
            timeLeft = newTimeLeft;
            timerText.text = timeLeft.ToString("0");
        }
        else
        {
            OnFinish.Invoke();
        }
    }
}
