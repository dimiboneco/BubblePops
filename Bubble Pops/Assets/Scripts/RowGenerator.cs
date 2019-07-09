using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowGenerator : MonoBehaviour
{
    public int[] GenerateRow(int size)
    {
        int[] myNumbers = new int[size];
        for (int i=0; i<myNumbers.Length; i++)
        {
            myNumbers[i] = (int) Mathf.Pow(2, Random.Range(1, 11));
        }
        return myNumbers;
    }

    public int GenerateOne()
    {
        var number = (int)Mathf.Pow(2, Random.Range(1, 11));
        return number;
    }
}
