using System;
using UnityEngine;

class Helpers: IDisposable
{
     public enum Importance
     {
          /// <summary>
          /// Informs only
          /// </summary>
          Low,
          /// <summary>
          /// Warns
          /// </summary>
          Medium,
          /// <summary>
          /// Throws an exception
          /// </summary>
          High
     }

     public void NullCheck<T>(T parameter, Importance criticalLevel)
     {
          if(parameter == null)
          {
               switch(criticalLevel)
               {
                    case Importance.Low:
                    {
                         Debug.Log($"NullCheck: {typeof(T)} is null");
                         break;
                    }
                    case Importance.Medium:
                    {
                         Debug.LogWarning($"NullCheck: {typeof(T)} is null");
                         break;
                    }
                    case Importance.High:
                    {
                         throw new Exception($"NullCheck: {typeof(T)} is null");
                    }
               }
          }
     }

#warning NullCheck.LogToFile() not implemented
     public void Dispose()
     {
     }
}