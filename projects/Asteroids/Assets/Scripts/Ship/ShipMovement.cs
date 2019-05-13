using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    float angularVelocity;
    public float rotationSpeed = 10;

    float thrust; // magnitude of forward thrust force
    public float thrustStrength = 10;

    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        angularVelocity = -Input.GetAxis("Horizontal") * rotationSpeed;
        float vert = Input.GetAxis("Vertical");
        if(vert >= 0)
        {
            thrust = vert * thrustStrength;
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(0,0,angularVelocity*Time.fixedDeltaTime);
        rb.AddForce(transform.up*thrust*Time.fixedDeltaTime);
    }
}
