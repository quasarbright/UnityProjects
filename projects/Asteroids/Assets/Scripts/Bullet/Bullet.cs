using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BulletDeath))]
public class Bullet : MonoBehaviour
{
    public int lifeSpan = 100;// in frame updates
    int timeLeft;

    BulletDeath death;
    void Start()
    {
        timeLeft = lifeSpan;
        death = GetComponent<BulletDeath>();
    }

    void OnEnable()
    {
        Start();
    }

    void Update()
    {
        if(timeLeft <= 0)
        {
            death.Die();
            return;
        }
        timeLeft--;
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    // }
}
