using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderHaver : MonoBehaviour
{
    public ComputeShader shader;
    public ComputeBuffer colorBuffer;
    public Camera mainCamera;
    public GameObject displayObject;
    public ComputeBuffer boundsBuffer;
    [SerializeField]
    public List<Color> colors;
    private Color[] colorsArray;
    public Vector2 zoomTo = new Vector2(0.4244f, 0.200759f);
    public float zoomRate = .1f;
    float xmin, xmax, ymin, ymax;
    Vector2 displayMin, displayMax;
    float displayWidth, displayHeight;
    int kernelNumber;
    
    public RenderTexture rt;

    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SystemInfo.supportsComputeShaders);

        Vector2 displayPosition = displayObject.transform.position;
        displayWidth = displayObject.transform.lossyScale.x;
        displayHeight = displayObject.transform.lossyScale.y;
        displayMin = new Vector2(displayPosition.x - displayWidth/2, displayPosition.y - displayHeight/2);
        displayMax = new Vector2(displayPosition.x + displayWidth/2, displayPosition.y + displayHeight/2);


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
        xmin = zoomTo.x-size;
        xmax = zoomTo.x+size;
        ymin = zoomTo.y-size;
        ymax = zoomTo.y+size;
        UpdateBounds();



        kernelNumber = shader.FindKernel("CSMain");
        rt = new RenderTexture(1024, 1024, 32);
        rt.enableRandomWrite = true;
        rt.Create();
        shader.SetTexture(kernelNumber, "Result", rt);
        shader.SetBuffer(kernelNumber, "colors", colorBuffer);
        shader.SetBuffer(kernelNumber, "bounds", boundsBuffer);
        material.mainTexture = rt;
    }

    // Update is called once per frame
    void Update()
    {
        shader.Dispatch(kernelNumber, 1024/8, 1024/8, 1);
        if(Input.GetMouseButton(0)) {
            ZoomToMouse();
        }
    }

    void Zoom()
    {
        xmin = Lerp(xmin, zoomTo.x, zoomRate);
        xmax = Lerp(xmax, zoomTo.x, zoomRate);
        ymin = Lerp(ymin, zoomTo.y, zoomRate);
        ymax = Lerp(ymax, zoomTo.y, zoomRate);
        UpdateBounds();
    }



    // only zooms if mouse in bounds
    void ZoomToMouse() 
    {
        
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector2 worldVector = hit.point;
            Vector2 complexVector = WorldToComplex(worldVector);
            zoomTo = complexVector;
            Zoom();
        }
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

    Vector2 WorldToComplex(Vector2 worldVector)
    {
        float re = xmin + (worldVector.x-displayMin.x) * (xmax - xmin) / displayWidth;
        float im = ymin + (worldVector.y-displayMin.y) * (ymax - ymin) / displayHeight;
        Debug.Log(worldVector.x);
        Debug.Log(re);
        Debug.Log(displayMin.x);
        return new Vector2(re, im);
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