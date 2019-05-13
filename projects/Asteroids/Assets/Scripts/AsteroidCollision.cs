using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Kill(GameObject go)
    {
        Death death = go.GetComponent<Death>();
        if(death != null)
        {
            death.Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        string othertag = other.tag;
        if(othertag == "Player" || othertag == "PlayerProjectile")
        {
            Kill(other);
        }
    }
}
