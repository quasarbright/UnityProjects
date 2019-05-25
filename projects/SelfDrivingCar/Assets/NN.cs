using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN
{
    Matrix w1;
    Matrix w2;
    public NN(int inputSize, int hiddenSize, int outputSize)
    {
        w1 = new Matrix(inputSize, hiddenSize);
        w2 = new Matrix(hiddenSize, outputSize);
    }

    void RandomizeWeights()
    {
        for(int r = 0; r < w1.rows; r++)
        {
            for(int c = 0; c < w1.cols; c++)
            {
                w1.Set(r, c, Random.Range(-1f,1f));
            }
        }

        for (int r = 0; r < w2.rows; r++)
        {
            for (int c = 0; c < w2.cols; c++)
            {
                w2.Set(r, c, Random.Range(-1f, 1f));
            }
        }
    }

    void MutateWeights(float mutationRate)
    {
        for (int r = 0; r < w1.rows; r++)
        {
            for (int c = 0; c < w1.cols; c++)
            {
                if(Random.Range(0f, 1f) < mutationRate)
                {
                    w1.Set(r, c, Random.Range(-1f, 1f));
                }
            }
        }

        for (int r = 0; r < w2.rows; r++)
        {
            for (int c = 0; c < w2.cols; c++)
            {
                if (Random.Range(0f, 1f) < mutationRate)
                {
                    w2.Set(r, c, Random.Range(-1f, 1f));
                }
            }
        }
    }

    Matrix FormatInputList(float[] input)
    {
        if(input.Length != w1.rows)
        {
            throw new System.ArgumentException("invalid dimensions for input");
        }
        Matrix ans = new Matrix(1, w1.rows);
        for(int i = 0; i < w1.rows; i++)
        {
            ans.Set(1, i, input[i]);
        }
        return ans;
    }

    public Matrix Forward(Matrix input)
    {
        Matrix ans = input.MatMul(w1);
        ans.ApplyFunc(x => ReLU(x));
        ans = ans.MatMul(w2);
        return ans;
    }

    public Matrix Forward(float[] input)
    {
        return Forward(FormatInputList(input));
    }

    float ReLU(float x)
    {
        if(x < 0)
        {
            return 0;
        }
        else
        {
            return x;
        }
    }
}
