using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationBehavior : MonoBehaviour
{
    [Tooltip("Number of guys to start with")]
    public int startSize = 100;
    [Range(0f, 1f)]
    [Tooltip("probability of reproducing each second")]
    public float reproductionChance = 0.1f;
    public ObjectPooler guyPool;
    GuyBehavior[] guys;
    public int numFoods = 100;
    public GameObject foodPrefab;
    GameObject[] foods;
    public int numPoisons = 100;
    public GameObject poisonPrefab;
    GameObject[] poisons;
    [Tooltip("A cube object representing the boundaries of the world")]
    public GameObject bounds;
    Vector3  minPos;
    Vector3 maxPos;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = bounds.transform.position;
        float l = bounds.transform.lossyScale.x;
        float w = bounds.transform.lossyScale.z;
        float h = bounds.transform.lossyScale.y;
        minPos = new Vector3(position.x - l / 2f, position.y - h / 2f, position.z - w / 2f);
        maxPos = new Vector3(position.x + l / 2f, position.y + h / 2f, position.z + w / 2f);

        GameObject[] guyObjects = guyPool.GetAll();
        guys = new GuyBehavior[guyObjects.Length];
        foods = new GameObject[numFoods];
        for(int i = 0; i < numFoods; i++)
        {
            foods[i] = Instantiate(foodPrefab);
        }
        poisons = new GameObject[numPoisons];
        for (int i = 0; i < numPoisons; i++)
        {
            poisons[i] = Instantiate(poisonPrefab);
        }
        for(int i = 0; i < guyObjects.Length; i++)
        {
            GameObject guyObject = guyObjects[i];
            guys[i] = guyObject.GetComponent<GuyBehavior>();
            guys[i].foods = foods;
            guys[i].poisons = poisons;
        }
        for(int i = 0; i < startSize; i++)
        {
            Spawn(guyPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < guys.Length; i++)
        {
            // left off here about to do reproduction
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(minPos.x, maxPos.x);
        position.z = Random.Range(minPos.z, maxPos.z);
        position.y = Random.Range(minPos.y, maxPos.y);
        return position;
    }

    void Spawn(ObjectPooler pool)
    {
        Vector3 position = GenerateSpawnPosition();
        pool.SpawnObject(position, Quaternion.LookRotation(Vector3.forward, Vector3.up));
    }
}
