using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDeath : MonoBehaviour, Death
{
    public GameObject parent;
    Death parentDeath;
    // Start is called before the first frame update
    void Start()
    {
        parentDeath = parent.GetComponent<Death>();
    }

    // ghosts kill their parents when they die
    public void Die()
    {
        if(parentDeath != null)
        {
            parentDeath.Die();
        }
    }
}
