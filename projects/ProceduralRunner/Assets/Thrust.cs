using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thrust : MonoBehaviour
{
    public float thrustStrength = 10;
    Rigidbody2D rb;
    bool shouldPush = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Jump"))
        {
            shouldPush = true;
        }
    }

    void FixedUpdate()
    {
        if(shouldPush)
        {
            rb.AddForce(Vector2.up * thrustStrength * Time.fixedDeltaTime);
            shouldPush = false;
        }
    }
}
