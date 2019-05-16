using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int seed;
    [Range(0, 1)]

    public static GameController instance;
    // Start is called before the first frame update
    void Awake()
    {
        seed = Random.Range(-999, 999);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
