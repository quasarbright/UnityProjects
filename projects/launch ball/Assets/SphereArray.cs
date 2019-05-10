using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereArray : MonoBehaviour
{
    public Rigidbody spherePrefab;
    public float speed = 100F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Spawn() {
        Rigidbody newSphere = Instantiate(spherePrefab, transform.position + transform.forward, transform.rotation);
        newSphere.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        // spheres.Add(newSphere);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Spawn();
        }
    }
}
