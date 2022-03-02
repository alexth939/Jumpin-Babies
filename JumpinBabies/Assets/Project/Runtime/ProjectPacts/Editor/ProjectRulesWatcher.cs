using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

namespace ProjectPact
{
     public static class ProjectRulesWatcher
     {
          [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
          private static void CheckProject()
          {
               CheckSceneNames();
               Debug.Log($"check done");
          }

          private static void CheckSceneNames()
          {
               // beware! this shit (unity 2021.2.11f1) will count current scene too,
               // even if u removed it from build but still holding it open in editor

               #region preparations
               int sceneCountInBuild = SceneManager.sceneCountInBuildSettings;

               var sceneNamesInProject = Enum.GetNames(typeof(SceneName)).ToList();
               sceneNamesInProject.Sort();

               int sceneCountDeclared = sceneNamesInProject.Count;
               //Debug.Log($"{string.Join('\n', sceneNamesInProject)}");

               var sceneNamesInBuild = new string[sceneCountInBuild].Select((result, i) =>
                    result = Path.GetFileNameWithoutExtension(
                         SceneUtility.GetScenePathByBuildIndex(i))).
                              ToList();
               sceneNamesInBuild.Sort();
               //Debug.Log($"{string.Join('\n', sceneNamesInBuild)}");

               string errorMessage = "Scene names in build differs from names in SceneNamesContainer!";
               #endregion

               if(sceneCountInBuild.Equals(sceneCountDeclared) == false)
                    throw new Exception(errorMessage);

               if(Enumerable.SequenceEqual(sceneNamesInBuild, sceneNamesInProject) == false)
                    throw new Exception(errorMessage);
          }
     }
}
