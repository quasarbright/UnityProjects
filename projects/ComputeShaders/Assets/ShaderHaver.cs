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
    double[] zoomTo = {0, 0};
    public float zoomRate = .1f;
    double xmin, xmax, ymin, ymax;
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

        boundsBuffer = new ComputeBuffer(4, sizeof(double));
        float size = 2;
        xmin = zoomTo[0]-size;
        xmax = zoomTo[0]+size;
        ymin = zoomTo[1]-size;
        ymax = zoomTo[1]+size;
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
            ZoomToMouse(true);
        } else if(Input.GetMouseButton(1) || Input.GetMouseButton(2)) {
            ZoomToMouse(false);
        }
    }

    void Zoom(bool zoomIn)
    {
        float rate = zoomRate;
        if(!zoomIn) {
            rate = -rate;
        }
        xmin = Lerp(xmin, zoomTo[0], rate);
        xmax = Lerp(xmax, zoomTo[0], rate);
        ymin = Lerp(ymin, zoomTo[1], rate);
        ymax = Lerp(ymax, zoomTo[1], rate);
        UpdateBounds();
    }



    // only zooms if mouse in bounds
    void ZoomToMouse(bool zoomIn) 
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector2 worldVector = hit.point;
            double[] complexVector = WorldToComplex(worldVector);

            zoomTo = complexVector;
            Zoom(zoomIn);
        }
    }

    void UpdateBounds()
    {
        double[] bounds = { xmin, xmax, ymin, ymax };
        boundsBuffer.SetData(bounds);
    }

    double Lerp(double a, double b, double r)
    {
        return a + (b - a) * r;
    }

    double[] WorldToComplex(Vector2 worldVector)
    {
        double re = xmin + (worldVector.x-displayMin.x) * (xmax - xmin) / displayWidth;
        double im = ymin + (worldVector.y-displayMin.y) * (ymax - ymin) / displayHeight;
        return new double[]{re, im};
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
