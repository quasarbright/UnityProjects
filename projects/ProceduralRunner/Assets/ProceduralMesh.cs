using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(EdgeCollider2D))]
public class ProceduralMesh : MonoBehaviour
{
    [Tooltip("how fine the mesh is")]
    [Range(2, 1000)]
    public int nx = 10;
    [Range(0f, 10f)]
    public float noiseRoughness = 5f;
    MeshFilter mf;
    EdgeCollider2D coll;
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] points;
    int[] triangles;
    float[] heights;
    int seed = 0;
    // Start is called before the first frame update
    void Start()
    {
        seed = Random.Range(-99999, 99999);
        mf = GetComponent<MeshFilter>();
        coll = GetComponent<EdgeCollider2D>();
        mesh = new Mesh();
        UpdateHeights();
        UpdateMesh();
        UpdateCollider();
        RecalculateNormals();
    }

    void UpdateHeights()
    {
        heights = new float[nx];
        float k = noiseRoughness / nx;
        for(int i = 0; i < nx; i++)
        {
            heights[i] = Mathf.PerlinNoise(i*k, seed);
        }
    }

    void UpdateMesh()
    {
        vertices = new Vector3[2*nx];
        triangles = new int[(nx-1)*6];
        float k = 1f / nx;
        for(int i = 0; i < nx; i++)
        {
            vertices[2*i] = new Vector3(i*k, 0, 0);
            vertices[2*i+1] = new Vector3(i*k, heights[i], 0);
        }

        for(int i = 0; i < nx-1; i++)
        {
            int dl = 2*i;
            int ul = 2*i+1;
            int dr = 2*i+2;
            int ur = 2*i+3;

            triangles[6 * i] = dl;
            triangles[6 * i + 1] = ul;
            triangles[6 * i + 2] = dr;

            triangles[6 * i + 3] = dr;
            triangles[6 * i + 4] = ul;
            triangles[6 * i + 5] = ur;
        }

        mesh.SetVertices(new List<Vector3>(vertices));
        mesh.SetTriangles(triangles, 0);
        mf.mesh = mesh;
    }

    void UpdateCollider()
    {
        points = new Vector2[nx];
        float k = 1f/nx;
        for(int i = 0; i < nx; i++)
        {
            points[i] = new Vector2(i*k, heights[i]);
        }
        coll.points = points;
    }

    void RecalculateNormals()
    {
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeights();
        UpdateMesh();
        UpdateCollider();
        RecalculateNormals();
    }
}
