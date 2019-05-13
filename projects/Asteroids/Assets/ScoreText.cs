using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = ScoreData.score.ToString();
    }

    void Update()
    {
        text.text = ScoreData.score.ToString();
    }
}
