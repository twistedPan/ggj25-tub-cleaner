using UnityEngine;

public static class Utilities
{
    public static float MapRange(this float n, float in_Start, float in_Stop, float out_Start, float out_Stop)
    {
        float value = (n - in_Start) / (in_Stop - in_Start) * (out_Stop - out_Start) + out_Start;
        if (out_Start < out_Stop)
            return value < out_Start ? out_Start : value > out_Stop ? out_Stop : value;
        else
            return value < out_Stop ? out_Stop : value > out_Start ? out_Start : value;
    }

    public static float RoundToDecimal(this float v, float dec)
    {
        return Mathf.Floor(v * Mathf.Pow(10, dec)) / Mathf.Pow(10, dec);
    }
}
