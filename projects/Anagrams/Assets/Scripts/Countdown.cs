using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Countdown : MonoBehaviour
{
    Text timerText;
    
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
        timeLeft -= Time.deltaTime;
        timerText.text = timeLeft.ToString("0");
    }
}
