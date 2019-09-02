using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTerrain : MonoBehaviour
{
    [Tooltip("The dimensions of the terrain, in number of cubes")]
    public int length, width, height;

    [Tooltip("Frequency of the noise. Higher means less smooth")]
    public float frequency = 0.1f;

    [Tooltip("Lower means less empty space")]
    [Range(0f, 1f)]
    public float threshold = 0.5f;

    [Tooltip("How quickly the world moves")]
    public float velocity = 1f;
    float offset;

    [Tooltip("The block the world will be constructed from")]
    public GameObject blockPrefab;

    GameObject[,,] blocks;

    PerlinNoise noise;
    // Start is called before the first frame update
    void Start()
    {
        offset = 0f;
        noise = new PerlinNoise(frequency);

        blocks = new GameObject[length,width,height];
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 pos = new Vector3(x, y, z); // TODO scale with the scale of the prefab
                    blocks[x, z, y] = Instantiate(blockPrefab, pos, Quaternion.LookRotation(Vector3.forward, Vector3.up));
                    // blocks[x, z, y].SetActive(false);
                    // float value = noise.Sample(x, y, z);
                    // Debug.Log(value);
                    // if (value >= threshold)
                    // {
                    //     GameObject block = blocks[x, y, z];
                    //     block.SetActive(true);
                    // }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        offset += velocity;
        // noise = new PerlinNoise(frequency);
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    float value = noise.Sample(x, y + offset, z);
                    // Debug.Log(value);
                    GameObject block = blocks[x, z, y];
                    if (value >= threshold)
                    {
                        block.SetActive(true);
                    }
                    else
                    {
                        block.SetActive(false);
                    }
                }
            }
        }
    }
}
