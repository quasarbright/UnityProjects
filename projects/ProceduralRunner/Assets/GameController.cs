using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject pausePanel;
    public int seed;
    [Range(0, 1)]
    bool paused = true;

    public static GameController instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(pausePanel == null)
        {
            Debug.LogWarning("game controller needs a pause canvas");
        }
        instance = this;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        seed = Random.Range(-999, 999);
    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    void UnPause()
    {
        paused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    void TogglePause()
    {
        if (paused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
        if(Input.GetButtonDown("Jump") && paused)
        {
            UnPause();
        }
    }
}
