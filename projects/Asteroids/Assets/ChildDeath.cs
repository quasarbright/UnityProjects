using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildDeath : MonoBehaviour, Death
{
    Death parentDeath;
    // Start is called before the first frame update
    void Start()
    {
        parentDeath = transform.parent.gameObject.GetComponent<Death>();
    }

    public void Die()
    {
        parentDeath.Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
