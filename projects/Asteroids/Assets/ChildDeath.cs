using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildDeath : MonoBehaviour, Death
{
    Death parentDeath;
    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            parentDeath = transform.parent.gameObject.GetComponent<Death>();
        }
    }

    public void Die()
    {
        if (parentDeath != null)
        {
            parentDeath.Die();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
