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

    [HideInInspector]
    public GameObject[] foods;
    [HideInInspector]
    public GameObject[] poisons;

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
    void FixedUpdate()
    {
        LookAtVelocity();
        if(foods != null)
        {
            Vector3 closest = new Vector3(-1000000, -100000, -10000);
            float closestDsq = float.PositiveInfinity;
            for(int i = 0; i < foods.Length; i++)
            {
                GameObject food = foods[i];
                if(food != null)
                {
                    Vector3 disp = food.transform.position - transform.position;
                    float distsq = disp.sqrMagnitude;
                    if(distsq < dna.foodRadius * dna.foodRadius && distsq < closestDsq)
                    {
                        closest = food.transform.position;
                        closestDsq = distsq;
                    }
                }
            }
            if(closest != new Vector3(-1000000, -100000, -10000))
            {
                Vector3 disp = closest - this.transform.position;
                Vector3 norm = disp.normalized;
                Vector3 force = norm * dna.foodStrength;
                force *= Time.fixedDeltaTime;
                rb.AddForce(force);
            }
        }
    }
}
