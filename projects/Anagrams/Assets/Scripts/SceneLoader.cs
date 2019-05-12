using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void NextScene() {
        int currInd = SceneManager.GetActiveScene().buildIndex;
        if(currInd < SceneManager.sceneCount - 1){
            SceneManager.LoadScene(currInd + 1);
        }
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
