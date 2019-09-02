using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{
    float frequency;
    float seed;

    public PerlinNoise(float frequency)
    {
        this.frequency = frequency;
        this.seed = Random.Range(-10000, 10000);
    }

    public float Sample(float x, float y)
    {
        return Mathf.PerlinNoise(x, y);        
    }

    public float Sample(float x, float y, float z)
    {
        x = x * this.frequency + seed;
        y = y * this.frequency + seed;
        z = z * this.frequency + seed;
        float ans = 0f;
        ans += this.Sample(x, y);
        ans += this.Sample(y, x);
        ans += this.Sample(x, z);
        ans += this.Sample(z, x);
        ans += this.Sample(y, z);
        ans += this.Sample(z, y);
        ans = ans / 6f;
        return ans;
    }
}
