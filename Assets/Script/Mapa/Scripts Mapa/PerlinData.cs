using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinData : MonoBehaviour
{
    public float fillPercent = 0.15f;

    public float scale_i;
    public float scale_j;
    public int[,] GenerateData(int w, int h)
    {
        scale_i = Random.Range(4f, 25f);
        int[,] mapData = new int[w, h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                float heighPercent = Mathf.Pow(1 - (float)j / (float)h, 3);

                float value = Mathf.PerlinNoise(
                    (float)i / scale_i,
                    (float)j / scale_i
                    ) - heighPercent;

                mapData[i, j] = value < this.fillPercent ? 1 : 0;
            }
        }
        return mapData;
    }
}
