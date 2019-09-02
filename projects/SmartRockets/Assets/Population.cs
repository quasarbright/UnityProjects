using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public int populationSize = 10;
    public float mutationRate = .05f;
    public GameObject guyPrefab;
    public GameObject target;
    private GameObject[] guys;
    // Start is called before the first frame update
    void Start()
    {
        // instantiate guys randomly
        this.guys = new GameObject[this.populationSize];
        for(int i = 0; i < this.populationSize; i++)
        {
            this.guys[i] = Instantiate(this.guyPrefab, this.transform.position, this.transform.rotation);
            this.guys[i].GetComponent<GuyBehavior>().target = this.target;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.IsGenerationDone())
        {
            this.NewGeneration();
        }
    }

    bool IsGenerationDone()
    {
        for (int i = 0; i < this.guys.Length; i++)
        {
            if(!this.guys[i].GetComponent<GuyBehavior>().dead)
            {
                return false;
            }
        }
        return true;
    }

    // end the current generation, run mating and mutation, and start next generation
    // fitness is calculated during this process
    void NewGeneration()
    {
        DNA[] newDNAs = this.GetNewDNAs();
        for (int i = 0; i < this.guys.Length; i++)
        {
            GuyBehavior guy = this.guys[i].GetComponent<GuyBehavior>();
            guy.ResetValues();
            guy.dna = newDNAs[i];
        }
    }

    DNA[] GetNewDNAs()
    {
        // we want to randomly pick mates weighting their fitness
        // to let's make a random distribution with fitnesses as probabilities
        float[] fitnesses = new float[this.populationSize];
        for(int i = 0; i < this.populationSize; i++)
        {
            GameObject guy = this.guys[i];
            float fitness = guy.GetComponent<GuyBehavior>().CalcFitness();
            fitnesses[i] = fitness;
        }
        // now let's use our random distribution
        DiscreteDistribution dist = new DiscreteDistribution(fitnesses);

        DNA[] children = new DNA[this.guys.Length];
        for (int i = 0; i < children.Length; i++)
        {
            // crossover and mutation
            int i1 = dist.sample();
            int i2 = dist.sample();
            DNA parent1 = this.guys[i1].GetComponent<GuyBehavior>().dna;
            DNA parent2 = this.guys[i1].GetComponent<GuyBehavior>().dna;
            DNA child = new DNA(parent1, parent2);
            child.Mutate(this.mutationRate);
            children[i] =  child;
        }
        return children;
    }
}
