using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HelperFunctions
{
    public static float Remap(float iMin, float iMax, float oMin, float oMax, float v) 
    {
        float t = Mathf.InverseLerp(iMin, iMax, v);
        return Mathf.Lerp(oMin, oMax, t);
    }
}
