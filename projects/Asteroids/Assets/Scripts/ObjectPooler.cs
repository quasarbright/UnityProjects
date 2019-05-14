using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    public Pool[] pools;
    Dictionary<string, Queue<GameObject>> poolDictionary;
    
    public static ObjectPooler instance;
    void Start()
    {
        instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.name += i.ToString();
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary[pool.name] = objectPool;
        }
    }

    public GameObject SpawnObject(string name, Vector2 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(name))
        {
            Debug.LogWarning("name " + name + " doesn't exist in object pooler");
            return null;
        }
        GameObject objToSpawn = poolDictionary[name].Dequeue();

        objToSpawn.SetActive(false);
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolDictionary[name].Enqueue(objToSpawn);
        return objToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
