using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void Restart()
    {
        ScoreData.score = 0;
        // reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
