﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationBehavior : MonoBehaviour
{
    public ObjectPooler guyPool;
    GuyBehavior[] guys;
    public ObjectPooler foodPool;
    GameObject[] foods;
    public ObjectPooler poisonPool;
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
        for(int i = 0; i < guyObjects.Length; i++)
        {
            GameObject guyObject = guyObjects[i];
            guys[i] = guyObject.GetComponent<GuyBehavior>();
        }
        foods = foodPool.GetAll();
        poisons = poisonPool.GetAll();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(minPos.x, maxPos.x);
        position.z = Random.Range(minPos.z, maxPos.z);
        position.y = Random.Range(minPos.y, maxPos.y);
        guyPool.SpawnObject(position, Quaternion.LookRotation(Vector3.forward, Vector3.up));
    }
}