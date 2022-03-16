using UnityEngine;

static class Extensions
{
    public static float pow(this float n)
    {
        return Mathf.Pow(n, 2.0f);
    }
    public static float pow(this float n, float f)
    {
        return Mathf.Pow(n, f);
    }
    public static float abs(this float n)
    {
        return Mathf.Abs(n);
    }
    public static float round(this float n)
    {
        return Mathf.Round(n);
    }
    public static int roundInt(this float n)
    {
        return Mathf.RoundToInt(n);
    }
}
