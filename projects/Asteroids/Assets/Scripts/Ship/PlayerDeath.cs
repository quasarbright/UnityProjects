using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour, Death
{
    public GameObject deathParticleSystem;
    public void Die()
    {
        Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
    }
}
