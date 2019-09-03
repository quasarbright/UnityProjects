using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int size = 10;
    public Queue<GameObject> pool;

    void Awake()
    {
        {
            pool = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.name += i.ToString();
                obj.SetActive(true);
                pool.Enqueue(obj);
            }
        }
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        GameObject objToSpawn = pool.Dequeue();

        objToSpawn.SetActive(false);
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        pool.Enqueue(objToSpawn);
        return objToSpawn;
    }

    public GameObject SpawnObject()
    {
        return SpawnObject(this.transform.position, this.transform.rotation);
    }

    public GameObject[] GetAll()
    {
        return pool.ToArray();
    }
}
