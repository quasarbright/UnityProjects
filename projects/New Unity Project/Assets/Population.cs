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
}
