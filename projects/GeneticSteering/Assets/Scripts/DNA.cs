using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public float foodRadius;
    public float poisonRadius;
    public float foodStrength;
    public float poisonStrength;
    public float maxVelocity;

    static float minRadius = 0f;
    static float maxRadius = 20f;
    static float minStrength = -10000f;
    static float maxStrength = 10000f;
    static float minMaxVelocity = 0f;
    static float maxMaxVelocity = 20f;

    public DNA()
    {
        foodRadius = Random.Range(minRadius, maxRadius);
        poisonRadius = Random.Range(minRadius, maxRadius);
        foodStrength = Random.Range(minStrength, maxStrength);
        poisonStrength = Random.Range(minStrength, maxStrength);
        maxVelocity = Random.Range(minMaxVelocity, maxMaxVelocity);
        // foodRadius = maxRadius;
        // poisonRadius = maxRadius;
        // foodStrength = maxStrength;
        // poisonStrength = maxStrength;
        // maxVelocity = maxMaxVelocity;
    }

    DNA(float foodRadius, float poisonRadius, float foodStrength, float poisonStrength, float maxVelocity)
    {
        this.foodRadius = foodRadius;
        this.poisonRadius = poisonRadius;
        this.foodStrength = foodStrength;
        this.poisonStrength = poisonStrength;
        this.maxVelocity = maxVelocity;
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
        maxVelocity = RandomChoice(parent1.maxVelocity, parent1.maxVelocity);
    }

    public void Mutate()
    {
        foodRadius += Random.Range(-1f,1f);
        foodRadius = Mathf.Clamp(foodRadius, minRadius, maxRadius);

        poisonRadius += Random.Range(-1f, 1f);
        poisonRadius = Mathf.Clamp(poisonRadius, minRadius, maxRadius);

        foodStrength += Random.Range(-10f, 10f);
        foodStrength = Mathf.Clamp(foodStrength, minStrength, maxStrength);

        poisonStrength += Random.Range(-10f, 10f);
        poisonStrength = Mathf.Clamp(poisonStrength, minStrength, maxStrength);

        maxVelocity = Random.Range(-1f, 1f);
        maxVelocity = Mathf.Clamp(maxVelocity, minMaxVelocity, maxMaxVelocity);
    }

    public DNA Clone()
    {
        return new DNA(foodRadius, poisonRadius, foodStrength, poisonStrength, maxVelocity);
    }
}
