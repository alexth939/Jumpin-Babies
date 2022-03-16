using UnityEngine;

static class Extensions
{
     public static float Pow(this float value)
     {
          return Mathf.Pow(value, 2.0f);
     }
     public static float Pow(this float value, float power)
     {
          return Mathf.Pow(value, power);
     }
     public static float Abs(this float value)
     {
          return Mathf.Abs(value);
     }
     public static float Round(this float value)
     {
          return Mathf.Round(value);
     }
     public static int RoundToInt(this float value)
     {
          return Mathf.RoundToInt(value);
     }
}
