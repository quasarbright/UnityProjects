using System;
using System.Collections;
using System.Collections.Generic;
public class Matrix
{
    private float[,] vals;
    public readonly int rows;
    public readonly int cols;
    public Matrix(float[,] vals)
    {
        this.vals = vals;
        rows = vals.GetLength(0);
        cols = vals.GetLength(1);
    }
    public Matrix(int rows, int cols)
    {
        vals = new float[rows, cols];
        this.rows = rows;
        this.cols = cols;
    }

    public float Get(int r, int c)
    {
        return vals[r, c]; 
    }

    public void Set(int r, int c, float val)
    {
        vals[r, c] = val;
    }

    public Matrix MatMul(Matrix other)
    {
        if(other == null)
        {
            throw new System.ArgumentNullException("other matrix in matmul cannot be null");
        }
        if(this.cols != other.rows)
        {
            throw new System.ArgumentException("matmul shape mismatch: ("+rows+", "+cols+") x ("+other.rows+", "+other.cols+")");
        }
        Matrix ans = new Matrix(this.rows, other.cols);
        for(int ansr = 0; ansr < ans.rows; ansr++)
        {
            for(int ansc = 0; ansc < ans.cols; ansc++)
            {
                float dotProduct = 0;
                for(int i = 0; i < this.cols; i++)
                {
                    // this.cols should be equal to other.rows
                    dotProduct += this.Get(ansr, i) * other.Get(i, ansc);
                }
                ans.Set(ansr, ansc, dotProduct);
            }
        }
        return ans;
    }

    public void ApplyFunc(Func<float, float> f)
    {
        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < cols; c++)
            {
                Set(r, c, f(Get(r, c)));
            }
        }
    }
}
