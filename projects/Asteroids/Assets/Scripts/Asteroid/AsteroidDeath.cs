using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDeath : MonoBehaviour, Death
{
    public string nextAsteroidName;
    public int scoreValue = 1000;
    public void Die()
    {
        if (nextAsteroidName != null && nextAsteroidName != "")
        {
            Debug.Log(name+" died and spawned 2 more");
            ObjectPooler objectPooler = ObjectPooler.instance;
            objectPooler.SpawnObject(nextAsteroidName, transform.position, Quaternion.identity);
            objectPooler.SpawnObject(nextAsteroidName, transform.position, Quaternion.identity);
        }
        ScoreData.score += scoreValue;
        gameObject.SetActive(false);
    }
}
