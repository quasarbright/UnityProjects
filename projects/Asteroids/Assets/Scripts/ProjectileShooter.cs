using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileShooter : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20;
    int projectilesQueued = 0;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            projectilesQueued++;
        }
    }

    void FixedUpdate()
    {
        while(projectilesQueued > 0)
        {
            GameObject bullet = ObjectPooler.instance.SpawnObject("bullet", transform.position, transform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if(bulletRb != null)
            {
                Vector2 newVelocity = transform.up * speed;
                Vector2 inheritedVelocity = rb.velocity;
                newVelocity += inheritedVelocity;
                bulletRb.velocity = newVelocity;
            }
            projectilesQueued--;
        }
    }
}
