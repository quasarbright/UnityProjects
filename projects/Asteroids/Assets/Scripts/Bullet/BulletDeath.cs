using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeath : MonoBehaviour, Death
{
    public void Die()
    {
        // Debug.Log(name+" has died");
        gameObject.SetActive(false);
    }
}
