using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int lifeSpan = 100;// in frame updates
    int timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = lifeSpan;
    }

    void Update()
    {
        if(timeLeft <= 0)
        {
            Destroy(gameObject);
            return;
        }
        timeLeft--;
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    // }
}
