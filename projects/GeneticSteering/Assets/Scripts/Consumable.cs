using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("bounding cube")]
    public GameObject bounds;
    Vector3 minPos, maxPos;
    void Start()
    {
        Vector3 position = bounds.transform.position;
        float l = bounds.transform.lossyScale.x;
        float w = bounds.transform.lossyScale.z;
        float h = bounds.transform.lossyScale.y;
        minPos = new Vector3(position.x - l / 2f, position.y - h / 2f, position.z - w / 2f);
        maxPos = new Vector3(position.x + l / 2f, position.y + h / 2f, position.z + w / 2f);
        Relocate();
    }// left off not being able to reference bounds in the prefab

    void Relocate()
    {
        transform.position = GenerateSpawnPosition();
    }

    void OnTriggerEnter(Collider collider)
    {
        Relocate();
    }

    Vector3 GenerateSpawnPosition()
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(minPos.x, maxPos.x);
        position.z = Random.Range(minPos.z, maxPos.z);
        position.y = Random.Range(minPos.y, maxPos.y);
        return position;
    }
}
