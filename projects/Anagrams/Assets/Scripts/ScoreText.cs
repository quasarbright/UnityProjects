using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = GameData.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GameData.score.ToString();
    }
}
