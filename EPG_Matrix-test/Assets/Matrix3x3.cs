using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AxisId
{
    Right, Up, Forward 
}

public class Matrix3x3
{
    public float[,] matrix = new float[3, 3];

    public Matrix3x3()
    {
        Array.Clear(matrix, 0, 9);
    }
    public Matrix3x3(float[,] matrix)
    {
        this.matrix = matrix;
    }
    public Matrix3x3(float[] m)
    {
        for (int i = 0; i < 9; i++)
        {
            this.matrix[i / 3, i % 3] = m[i];
        }
    }

    public static int Add(int a, int b) { return a + b; }

    public static Matrix3x3 Rotate(float angleInDeg, AxisId axisId)
    {
        float andleInRad = Mathf.Deg2Rad * angleInDeg;
        float cos = Mathf.Cos(andleInRad);
        float sin = Mathf.Sin(andleInRad);

        float[,] matrix = new float[3, 3];
        Array.Clear(matrix, 0, 9);

        if (axisId == AxisId.Right)
        {
            matrix[0, 0] = 1;
            matrix[1, 1] = cos; matrix[1, 2] = -sin;
            matrix[2, 1] = sin; matrix[2, 2] = cos;
        }
        else if (axisId == AxisId.Up)
        {
            matrix[1, 1] = 1;
            matrix[0, 0] = cos; matrix[0, 2] = sin;
            matrix[2, 0] = -sin; matrix[2, 2] = cos;
        }
        else
        {
            matrix[2, 2] = 1;
            matrix[0, 0] = cos; matrix[0, 1] = -sin;
            matrix[1, 0] = sin; matrix[1, 1] = cos;
        }
        return new Matrix3x3(matrix);
    }

    public static Matrix3x3 Euler(float x, float y, float z)
    {
        return Rotate(z, AxisId.Forward) * Rotate(x, AxisId.Right) * Rotate(y, AxisId.Up);
    }


    public static Matrix3x3 Euler(Vector3 eulerAngles)
    {
        return Rotate(eulerAngles.y, AxisId.Up) 
            * Rotate(eulerAngles.x, AxisId.Right)
            * Rotate(eulerAngles.z, AxisId.Forward);
    }

    public static Vector3 operator *(Matrix3x3 m, Vector3 v)
    {
        Vector3 resultV = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                resultV[i] += m.matrix[i, j] * v[j];
            }
        }
        return resultV;
    }
    public static Matrix3x3 operator *(Matrix3x3 m0, Matrix3x3 m1)
    {
        Matrix3x3 resultM = new Matrix3x3();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                resultM.matrix[i/3, i%3] += m0.matrix[i/3, j] * m1.matrix[j, i%3];
            }
        }
        return resultM;
    }

    override
    public string ToString() {
        string result ="[ ";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                result += matrix[i, j];
                if (i != 2 || j != 2)
                    result += ", ";
                if (j == 2 && i != 2)
                    result += '\n';
            }
        }
        return result + " ]";
    }
}
