using UnityEngine;
using JumpinBabies.MainMenu;

namespace JumpinBabies.SceneWorkers
{
     sealed class MainSceneWorker: SceneWorker
     {
          [SerializeField] private ScreenCoverer screenCoverer;

          protected override void EnteringScene()
          {
               (float blackCover, float fakeCover) revealDurations = (2.0f, 3.0f);
               (float menuInit, float fakeCover) delays = (2.0f, 1.0f);

               var revealingScenario = new Scenario();

               revealingScenario.AddAct(() => screenCoverer.FadeIn(revealDurations.blackCover));
               revealingScenario.AddDelay(delays.menuInit);

               revealingScenario.AddAct(() => new MainMenuPresenter());
               revealingScenario.AddDelay(delays.fakeCover);
               revealingScenario.AddAct(() => screenCoverer.FadeIn(revealDurations.fakeCover));

               revealingScenario.Play(CoroutineOwner: this);
          }
     }
}
