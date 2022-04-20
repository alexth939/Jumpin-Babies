using System;

namespace UnityEngine.SceneManagement
{
     public enum SceneName
     {
          RefactorStartupScene,
          RefactorMainScene,
          RefactorGameScene,
          MainMenuScene,
          GameScene,
     }

     internal static class SceneManagementExtensions
     {
          public static string AsString(this SceneName name)
          {
               return Enum.GetName(typeof(SceneName), name);
          }
     }
}
