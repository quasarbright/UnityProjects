using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDeath : MonoBehaviour, Death
{
    public string nextAsteroidName;
    public void Die()
    {
        if (nextAsteroidName != null && nextAsteroidName != "")
        {
            ObjectPooler objectPooler = ObjectPooler.instance;
            objectPooler.SpawnObject(nextAsteroidName, transform.position, Quaternion.identity);
            objectPooler.SpawnObject(nextAsteroidName, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
