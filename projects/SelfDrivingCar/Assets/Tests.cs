using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour
{
    void Start()
    {
        Debug.Log("matmul: "+TestMatMul().ToString());
    }
    bool TestMatMul()
    {
        float[,] vals1 = new float[3, 2]
        {
            {1, 2},
            {3, 4},
            {5, 6},
        };
        Matrix m1 = new Matrix(vals1);

        float[,] vals2 = new float[2, 4]
        {
            {7,8,9,10},
            {11,12,13,14}
        };
        Matrix m2 = new Matrix(vals2);
        Matrix actual = m1.MatMul(m2);
        float[,] expectedVals = new float[3, 4]
        {
            {29,32,35,38},
            {65,72,79,86},
            {101,112,123,134}
        };
        Matrix expected = new Matrix(expectedVals);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (expected.Get(i, j) != actual.Get(i, j))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
