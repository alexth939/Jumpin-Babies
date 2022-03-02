using System;

namespace MoreUnityEngine
{
     public class MoreObject
     {
          public static T FindSingleSceneObject<T>(bool includeInactive = false) where T : UnityEngine.Object
          {
               var searchResults = UnityEngine.Object.FindObjectsOfType<T>(includeInactive: includeInactive);
               if(searchResults.Length != 1)
                    throw new Exception($"scene contains more or less than one {typeof(T)} components");

               return searchResults[0];
          }
     }
}
