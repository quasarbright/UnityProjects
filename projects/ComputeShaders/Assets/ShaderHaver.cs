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
    public Vector2 zoomTo;
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
        float xmin, xmax, ymin, ymax;
        xmin = -2;
        xmax = 2;
        ymin = -2;
        ymax = 2;
        float[] bounds = {xmin, xmax, ymin, ymax};
        boundsBuffer.SetData(bounds);
    }

    // Update is called once per frame
    void Update()
    {
        int kernelNumber = shader.FindKernel("CSMain");
        rt = new RenderTexture(1024, 1024, 32);
        rt.enableRandomWrite = true;
        rt.Create();
        shader.SetTexture(kernelNumber, "Result", rt);
        shader.SetBuffer(kernelNumber, "colors", colorBuffer);
        shader.SetBuffer(kernelNumber, "bounds", boundsBuffer);
        shader.Dispatch(kernelNumber, 1024/8, 1024/8, 1);
        material.mainTexture = rt;
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