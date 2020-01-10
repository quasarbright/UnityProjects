using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderHaver : MonoBehaviour
{
    public ComputeShader shader;
    public ComputeBuffer colorBuffer;
    public ComputeBuffer boundsBuffer;
    [SerializeField]
    public List<Color> colors;
    private Color[] colorsArray;
    public Vector2 zoomTo = new Vector2(0.4244f, 0.200759f);
    public float zoomRate = .1f;
    float xmin, xmax, ymin, ymax;
    int kernelNumber;
    
    public RenderTexture rt;

    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SystemInfo.supportsComputeShaders);


        colorsArray = new Color[256];
        colorBuffer = new ComputeBuffer(colorsArray.Length, 4 * 4);
        for(int i = 0; i < colorsArray.Length; i++) {
            int listIndex = i % colors.Count;
            colorsArray[i] = colors[listIndex];
        }
        colorsArray[colorsArray.Length-1] = new Color(0,0,0);
        colorBuffer.SetData(colorsArray);

        boundsBuffer = new ComputeBuffer(4, sizeof(float));
        float size = 2;
        xmin = zoomTo.x-2;
        xmax = zoomTo.x+2;
        ymin = zoomTo.y-2;
        ymax = zoomTo.y+2;
        UpdateBounds();



        kernelNumber = shader.FindKernel("CSMain");
        rt = new RenderTexture(1024, 1024, 32);
        rt.enableRandomWrite = true;
        rt.Create();
        shader.SetTexture(kernelNumber, "Result", rt);
        shader.SetBuffer(kernelNumber, "colors", colorBuffer);
        shader.SetBuffer(kernelNumber, "bounds", boundsBuffer);
    }

    // Update is called once per frame
    void Update()
    {
        shader.Dispatch(kernelNumber, 1024/8, 1024/8, 1);
        material.mainTexture = rt;
        Zoom();
    }

    void Zoom()
    {
        xmin = Lerp(xmin, zoomTo.x, zoomRate);
        xmax = Lerp(xmax, zoomTo.x, zoomRate);
        ymin = Lerp(ymin, zoomTo.y, zoomRate);
        ymax = Lerp(ymax, zoomTo.y, zoomRate);
        UpdateBounds();
    }

    void UpdateBounds()
    {
        float[] bounds = { xmin, xmax, ymin, ymax };
        boundsBuffer.SetData(bounds);
    }

    float Lerp(float a, float b, float r)
    {
        return a + (b - a) * r;
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderHaver : MonoBehaviour
{
    public ComputeShader shader;
    public ComputeBuffer colorBuffer;
    [SerializeField]
    public List<Color> colors;
    private float[] Rs;
    private float[] Gs;
    private float[] Bs;
    public RenderTexture rt;

    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SystemInfo.supportsComputeShaders);
        int numColors = 256;
        Rs = new float[256];
        Gs = new float[256];
        Bs = new float[256];
        ComputeBuffer buffer = new ComputeBuffer(, 4 * 4);
        for(int i = 0; i < numColors; i++) {
            int listIndex = i * colors.Count / colorsArray.Length;
            Color color = colors[listIndex];
            float r = color.r;
            float g = color.g;
            float b = color.b;
            colorsArray[i] = colors[listIndex];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        int kernelNumber = shader.FindKernel("CSMain");
        rt = new RenderTexture(256, 256, 32);
        rt.enableRandomWrite = true;
        rt.Create();
        shader.SetTexture(kernelNumber, "Result", rt);
        shader.SetBuffer(kernelNumber, "colors", colorBuffer);
        shader.Dispatch(kernelNumber, 256/8, 256/8, 1);
        material.mainTexture = rt;
    }
}
*/