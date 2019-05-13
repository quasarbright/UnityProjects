using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 1f; // units per second

    [SerializeField]
    public string[] spawnables; // each spawnable is equally likely

    float screenWidth;
    float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        // bottom left of the world
        Vector3 dl = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        // top right of the world
        Vector3 ur = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        screenWidth = ur.x - dl.x;
        screenHeight = ur.y - dl.y;
    }

    // Update is called once per frame
    void Update()
    {
        float rand = Random.Range(0f,1f);
        if(rand < spawnRate*Time.deltaTime)
        {
            SpawnAsteroid();
        }
    }

    Vector2 SpawnLocation()
    {
        float x = Random.Range(-screenWidth/2f, screenWidth/2f);
        float y = Random.Range(-screenHeight/2f, screenHeight/2f);
        return new Vector2(x, y);
    }

    GameObject SpawnAsteroid()
    {
        int index = Random.Range(0, spawnables.Length);
        string name = spawnables[index];
        return ObjectPooler.instance.SpawnObject(name, SpawnLocation(), Quaternion.identity);
    }
}
