using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDeath : MonoBehaviour, Death
{
    public void Die()
    {
        Destroy(gameObject);
    }
}
