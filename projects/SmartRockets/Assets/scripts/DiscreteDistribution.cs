﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteDistribution
{
    float[] totals;
    float total = 0;
    // these probabilities do not necessarilly have to be from 0 to 1 or sum to 1
    // but they must be positive
    public DiscreteDistribution(float[] probs)
    {
        this.totals = new float[probs.Length];
        for(int i = 0; i < probs.Length; i++)
        {
            Debug.Assert(probs[i] >= 0);
            this.total += (probs[i]);
            this.totals[i] = total;
        }
    }
    
    // returns the index of a randomly sampled element
    public int sample()
    {
        float rand = Random.Range(0f, this.total);
        for (int i = 0; i < this.totals.Length; i++)
        {
            if(rand <= this.totals[i])
            {
                return i;
            }
        }
        return this.totals.Length - 1;
    }
}
