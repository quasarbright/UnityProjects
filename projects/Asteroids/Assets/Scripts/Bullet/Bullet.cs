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

    void Update()
    {
        if(timeLeft <= 0)
        {
            death.Die();
            return;
        }
        timeLeft--;
    }

    static void Kill(GameObject go)
    {
        Death death = go.GetComponent<Death>();
        if (death != null)
        {
            death.Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        string othertag = other.tag;
        if(othertag == "Enemy")
        {
            Kill(other);
        }
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    // }
}
