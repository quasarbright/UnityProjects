using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Canvas pauseCanvas;
    public int seed;
    [Range(0, 1)]
    bool paused = true;

    public static GameController instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(pauseCanvas == null)
        {
            Debug.LogWarning("game controller needs a pause canvas");
        }
        instance = this;
        seed = Random.Range(-999, 999);
    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseCanvas.gameObject.SetActive(true);
    }

    void UnPause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseCanvas.gameObject.SetActive(false);
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
        if(paused)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
        if(Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
        if(Input.GetButtonDown("Jump"))
        {
            UnPause();
        }
    }
}
