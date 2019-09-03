using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GuyBehavior : MonoBehaviour
{
    public ObjectPooler pooler;
    float health;
    bool dead;
    DNA dna;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 100;
        dead = false;
        dna = new DNA();
        rb.velocity = Vector3.up;
    }

    void LookAtVelocity()
    {
        if(rb.velocity.sqrMagnitude > 0){
            this.transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAtVelocity();
    }
}
