using System;
using UnityEngine;

class Helpers
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

     public static void ConfirmNotNull<T>(T itemToCheck, Importance criticalLevel, string customMessage ="")
     {
          if(itemToCheck is null)
          {
               var resultMessage = GenerateNullResultMessage(wasNegativeCheck: true, customMessage, typeof(T));
               ShowNullCheckResults(resultMessage, criticalLevel);
          }
     }
     public static void ConfirmNull<T>(T itemToCheck, Importance criticalLevel, string customMessage = "")
     {
          if(itemToCheck is null)
               return;

          var resultMessage = GenerateNullResultMessage(wasNegativeCheck: false, customMessage, typeof(T));
          ShowNullCheckResults(resultMessage, criticalLevel);
     }
     private static string GenerateNullResultMessage(bool wasNegativeCheck, string customMessage, Type itemType)
     {
          string negativeInsertation = wasNegativeCheck ? "not " : string.Empty;
          return $"NullCheck: {itemType} is {negativeInsertation}null. {customMessage}";
     }
     private static void ShowNullCheckResults(string resultsMessage, Importance criticalLevel)
     {
          switch(criticalLevel)
          {
               case Importance.Low:
               {
                    Debug.Log(resultsMessage);
                    break;
               }
               case Importance.Medium:
               {
                    Debug.LogWarning(resultsMessage);
                    break;
               }
               case Importance.High:
               {
                    throw new Exception(resultsMessage);
               }
          }
     }
}
