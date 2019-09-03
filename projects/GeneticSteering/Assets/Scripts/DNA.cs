using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public float foodRadius;
    public float poisonRadius;
    public float foodStrength;
    public float poisonStrength;

    static float minRadius = 0f;
    static float maxRadius = 20f;
    static float minStrength = -20f;
    static float maxStrength = 20f;

    public DNA()
    {
        foodRadius = Random.Range(minRadius, maxRadius);
        poisonRadius = Random.Range(minRadius, maxRadius);
        foodStrength = Random.Range(minStrength, maxStrength);
        poisonStrength = Random.Range(minStrength, maxStrength);
    }

    DNA(float foodRadius, float poisonRadius, float foodStrength, float poisonStrength)
    {
        this.foodRadius = foodRadius;
        this.poisonRadius = poisonRadius;
        this.foodStrength = foodStrength;
        this.poisonStrength = poisonStrength;
    }

    float RandomChoice(float f1, float f2)
    {
        if(Random.Range(0f, 1f) < 0.5)
        {
            return f1;
        }
        else
        {
            return f2;
        }
    }

    public DNA(DNA parent1, DNA parent2)
    {
        foodRadius = RandomChoice(parent1.foodRadius, parent2.foodRadius);
        poisonRadius = RandomChoice(parent1.poisonRadius, parent2.poisonRadius);
        foodStrength = RandomChoice(parent1.foodStrength, parent2.foodStrength);
        poisonStrength = RandomChoice(parent1.poisonStrength, parent2.poisonStrength);
    }

    public void Mutate(float mutationRate)
    {
        if(Random.Range(0f, 1f) < mutationRate)
        {
            foodRadius = Random.Range(minRadius, maxRadius);
        }
        if(Random.Range(0f, 1f) < mutationRate)
        {
            poisonRadius = Random.Range(minRadius, maxRadius);
        }
        if(Random.Range(0f, 1f) < mutationRate)
        {
            foodStrength = Random.Range(minStrength, maxStrength);
        }
    }
}
