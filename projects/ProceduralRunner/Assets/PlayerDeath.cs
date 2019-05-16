using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        if(panel == null)
        {
            Debug.LogWarning("player death needs a panel");
        }
        else
        {
            panel.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Time.timeScale = 0;
        Debug.Log("F-Mega");
        panel.SetActive(true);
    }
}
