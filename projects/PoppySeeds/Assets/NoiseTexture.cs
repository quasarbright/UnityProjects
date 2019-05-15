using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class NoiseTexture : MonoBehaviour
{
    public int width = 256;

    public int height = 256;
    // Start is called before the first frame update

    public float spaceScale = 1f;
    public float timeScale = 0.1f;

    Vector2 offset = new Vector2(0,0);
    RawImage img;
    void Start()
    {
        img = GetComponent<RawImage>();
        img.texture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color color = CalcColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalcColor(int x, int y)
    {
        
        float hu = 0;//Mathf.PerlinNoise(x*spaceScale/width, y*spaceScale/height);
        hu += Mathf.PerlinNoise(x*spaceScale/width + offset.x-1000, y*spaceScale/height + offset.y-1000);
        hu += Mathf.PerlinNoise(x*spaceScale/width - offset.x+1000, y*spaceScale/height - offset.y+1000);
        hu = hu % 1f;
        return Color.HSVToRGB(hu, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        img.texture = GenerateTexture();
        offset += Vector2.right * timeScale * Time.deltaTime;
    }
}
