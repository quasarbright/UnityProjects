using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [Tooltip("number of x vertices")]
    public int nx = 50;
    [Tooltip("number of z vertices")]
    public int nz = 50;

    [Tooltip("higher means rougher noise")]
    public float noiseRoughness = 0.3f;

    Mesh mesh;
    MeshFilter filter;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
        CreateGrid();
        UpdateMesh();
    }

    void CreateGrid()
    {
        float dx = 1f / nx;
        float dz = 1f / nz;
        vertices = new Vector3[nz*nx];
        uvs = new Vector2[nz*nx];
        triangles = new int[(nx-1)*(nz-1)*3*2];
        // for(float z = 0; z <= height; z += resolution)
        int i = 0;
        for(int r = 0; r < nz; r++)
        {
            float z = r * dz;
            for(int c = 0; c < nx; c++)
            {
                float x = c * dx;
                float y = Mathf.PerlinNoise(noiseRoughness * x, noiseRoughness * z);
                vertices[i] = new Vector3(x, y, z);
                uvs[i] = new Vector2(x, z);
                i++;
            }
        }

        int triangleIndex = 0;
        for(int r = 0; r < nz-1; r++)
        {
            for(int c = 0; c < nx-1; c++)
            {
                int currentIndex = r*nx + c;
                // Gizmos.DrawSphere(vertices[currentIndex], .05f);
                int rightIndex = r*nx + (c+1);
                int upRightIndex = (r+1)*nx + (c+1);
                int upIndex = (r+1)*nx + c;

                triangles[triangleIndex] = currentIndex;
                triangleIndex++;
                triangles[triangleIndex] = upRightIndex;
                triangleIndex++;
                triangles[triangleIndex] = rightIndex;
                triangleIndex++;

                triangles[triangleIndex] = currentIndex;
                triangleIndex++;
                triangles[triangleIndex] = upIndex;
                triangleIndex++;
                triangles[triangleIndex] = upRightIndex;
                triangleIndex++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        CreateGrid();
        UpdateMesh();
    }

    // int j = 0;

    private void OnDrawGizmos()
    {
        // for (int r = 0; r < nz - 1; r++)
        // {
        //     for (int c = 0; c < nx - 1; c++)
        //     {
        //         Vector3 v = vertices[r*nx+c];
        //         if(Random.Range(0f, 1f) < 1f/nz){
        //             Gizmos.DrawSphere(v, .1f);
        //         }
        //     }
        // }
        // j = j % vertices.Length;
        // Gizmos.DrawSphere(vertices[vertices.Length-1-j], .5f);
        // j++;
    }
}
