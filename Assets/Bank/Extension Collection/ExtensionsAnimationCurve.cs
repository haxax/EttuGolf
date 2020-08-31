using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsAnimationCurve
{
    public static AnimationCurve Preset(this AnimationCurve ac, float value)
    {
        ac.preWrapMode = WrapMode.ClampForever;
        ac.postWrapMode = WrapMode.ClampForever;
        ac.AddKey(0, value);
        ac.AddKey(1, value);
        return ac;
    }

    public static float Evaluate(this AnimationCurve ac, float time, float magnitude)
    { return ac.Evaluate(time) * magnitude; }

    public static float EvaluateDividive(this AnimationCurve curve, float time, float magnitude)
    {
        float result = curve.Evaluate(time);
        result *= magnitude;
        if (result > 0)
        {
            result++;
        }
        else if (result < 0 && magnitude != 0)
        {
            result--;
            result = 1 / -result;
        }
        else
        {
            result = 1;
        }
        return result;
    }
}