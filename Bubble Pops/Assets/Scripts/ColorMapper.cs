using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorMapper : MonoBehaviour
{
    public List<Color> ColorList;
    public List<int> NumberList;
    
    public Color MatchNumberToColor(int number)
    {
        for(int i=0; i<NumberList.Count; i++)
        {
            if (number == NumberList[i])
            {
                return ColorList[i];
            }
        }
        throw new ArgumentException();
    }
}
