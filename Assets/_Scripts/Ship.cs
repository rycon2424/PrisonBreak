using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public bool hasPart1, hasPart2, hasPart3, hasPart4;
    public static bool hasPart1Static, hasPart2Static, hasPart3Static, hasPart4Static;
    
    void Update()
    {
        if (hasPart1)
        {
            hasPart1Static = true;
        }
        if (hasPart2)
        {
            hasPart2Static = true;
        }
        if (hasPart3)
        {
            hasPart3Static = true;
        }
        if (hasPart4)
        {
            hasPart4Static = true;
        }
    }

}
