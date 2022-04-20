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

               var attemptToLeave = GenerateCounterDelayedAction(delayCounter: 2, () => SwitchScene(SceneName.RefactorMainScene));

               gameSettings.ImportAsync(onImported: () =>
                    gameSettings.ApplyAsync(onApplied: attemptToLeave));

               gameProgress.ImportAsync(onImported: attemptToLeave);
          }

          protected override void OnApplicationAcquiredFocus()
          {
               //do nothing
          }

          protected override void OnApplicationLostFocus()
          {
               //do nothing
          }

          protected override void PrepareLeaveScene()
          {
               throw new System.NotImplementedException();
          }
     }
}
