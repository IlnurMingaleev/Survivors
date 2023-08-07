using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    public static int RandomSignInt(int offset)
    {
        int positiveOffset = Mathf.Abs(offset);
        int result = (Random.value > 0.5) ? positiveOffset : -positiveOffset;
        return result;
    }
}
