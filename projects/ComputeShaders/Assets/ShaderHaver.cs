using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderHaver : MonoBehaviour
{
    public ComputeShader shader;
    public RenderTexture rt;

    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SystemInfo.supportsComputeShaders);
    }

    // Update is called once per frame
    void Update()
    {
        int kernelNumber = shader.FindKernel("CSMain");
        rt = new RenderTexture(256, 256, 32);
        rt.enableRandomWrite = true;
        rt.Create();
        shader.SetTexture(kernelNumber, "Result", rt);
        shader.Dispatch(kernelNumber, 256/8, 256/8, 1);
        material.mainTexture = rt;
    }
}
