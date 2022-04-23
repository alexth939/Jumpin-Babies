using GameData;
using UnityEngine.SceneManagement;
using static System.Threading.Tasks.DelegateGenerator;

namespace JumpinBabies.SceneWorkers
{
     public class StartupSceneWorker: SceneWorker
     {
          protected override void SetupScene()
          {
               var gameSettings = GameSettingsProvider.SingleInstance;
               var gameProgress = GameProgressProvider.SingleInstance;

               var attemptToLeave = GenerateCounterDelayedAction(delayCounter: 1, () => SwitchScene(SceneName.RefactorMainScene));

               gameSettings.ImportAsync(onImported: () =>
                    gameSettings.ApplyAsync(onApplied: attemptToLeave));

               gameProgress.ImportAsync(onImported: attemptToLeave);
          }
     }
}
