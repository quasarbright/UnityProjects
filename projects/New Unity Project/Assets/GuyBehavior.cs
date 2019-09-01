using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is responsible for reading DNA and moving based on it
// it knows when to die and how to calculate fitness
// it never triggers crossover or mutation and doesn't know about other Guys
[RequireComponent(typeof(Rigidbody))]
public class GuyBehavior : MonoBehaviour
{
    // maximum age
    public int lifespan = 100;
    // how strongly to apply the forces
    public float forceMagnitude = 10;
    // age increments every tick and we die when we hit our lifespan
    int age = 0;
    public GameObject target;
    // contains force sequence
    public DNA dna;
    Rigidbody rb;
    // should we keep moving?
    public bool dead = false;
    // have we hit the target?
    public bool succeeded = false;
    // Start is called before the first frame update
    void Start()
    {
        dna = new DNA(lifespan);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // face the direction we're moving
        this.transform.rotation = Quaternion.LookRotation(this.rb.velocity, Vector3.up);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(Object.ReferenceEquals(collision.gameObject, this.target))
        {
            // we hit the target
            this.succeeded = true;
            this.dead = true;
        }
    }

    void FixedUpdate()
    {
        if(!dead)
        {
            // apply current force and increment age
            Vector3 force = dna.forces[this.age];
            force = force * Time.fixedDeltaTime * 100;
            force = force * this.forceMagnitude;
            this.rb.AddForce(force);
            this.age++;
            if(this.age == this.lifespan)
            {
                this.dead = true;
            }
        }
        else
        {
            //we're dead. freeze
            this.rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public float CalcFitness()
    {
        // we want a fitness function that is inversely related to distance from target
        // so the closer to the target, the more fitness
        Vector3 disp = this.target.transform.position - this.transform.position;
        float fitness = 20*20 - disp.sqrMagnitude;
        if(fitness < 0)
        {
            return 0;
        }
        else{
            return fitness;
        }
    }

    public void ResetValues()
    {
        // keep dna information, but go back to start
        this.age = 0;
        this.dead = false;
        this.succeeded = false;
        this.transform.position = Vector3.zero;
        this.rb.constraints = RigidbodyConstraints.None;
        this.rb.velocity = Vector3.zero;
        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }
}
