using GameData;
using UnityEngine.SceneManagement;
using static System.Threading.Tasks.DelegateGenerator;

namespace JumpinBabies.SceneWorkers
{
     internal sealed class StartupSceneWorker: SceneWorker
     {
          protected override void EnteringScene()
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
