using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public Vector3[] forces;

    public int Length
    {
        get
        {
            return forces.Length;
        }
    }

    public DNA(int length)
    {
        // initialize forces randomly with magnitude 1
        this.forces = new Vector3[length];
        for(int i = 0; i < length; i++)
        {
            this.forces[i] = Random.onUnitSphere;
        }
    }

    public DNA(DNA parent1, DNA parent2)
    {
        // create DNA based on two parents
        Debug.Assert(parent1.Length == parent2.Length);
        int length = parent1.Length;
        Vector3[] newForces = new Vector3[length];
        for(int i = 0; i < length; i++)
        {
            if(Random.Range(0.0f, 1.0f) < 0.5f)
            {
                newForces[i] = parent1.forces[i];
            }
            else
            {
                newForces[i] = parent2.forces[i];
            }
        }
        this.forces = newForces;
    }

    public void Mutate(float mutationRate)
    {
        // mutation rate is a probability from 0 to 1 of each gene randomly mutating
        // this method randomly mutates this DNA's genes according to the mutation rate
        for(int i = 0; i < this.Length; i++)
        {
            if(Random.Range(0f, 1f) < mutationRate)
            {
                // mutate this gene
                this.forces[i] = Random.onUnitSphere;
            }
        }
    }
}
