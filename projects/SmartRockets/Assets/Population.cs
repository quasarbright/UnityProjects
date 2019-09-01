using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public int populationSize = 10;
    public GameObject guyPrefab;
    private GameObject[] guys;
    // Start is called before the first frame update
    void Start()
    {
        // instantiate guys randomly
        this.guys = new GameObject[this.populationSize];
        for(int i = 0; i < this.populationSize; i++)
        {
            this.guys[i] = Instantiate(this.guyPrefab, this.transform.position, this.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // end the current generation, run mating and mutation, and start next generation
    // fitness is calculated here
    void NewGeneration()
    {

    }

    DNA[] getNewDNAs()
    {
        // we want to randomly pick mates weighting their fitness
        // to let's make a random distribution with fitnesses as probabilities
        float[] fitnessTotals = new float[this.populationSize];
        float fitnessTotal = 0;
        for(int i = 0; i < this.populationSize; i++)
        {
            GameObject guy = this.guys[i];
            float fitness = guy.GetComponent<GuyBehavior>().CalcFitness();
            fitnessTotal += fitness;
            fitnessTotals[i] = fitnessTotal;
        }

        // now let's use our random distribution
    }
}
