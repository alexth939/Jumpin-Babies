using UnityEngine;
using JumpinBabies.MainMenu;

namespace JumpinBabies.SceneWorkers
{
     sealed class MainSceneWorker: SceneWorker
     {
          [SerializeField] private ScreenCoverer _screenCoverer;

          protected override void EnteringScene()
          {
               float blackCoverDuration = 2.0f;
               float fakeCoverDuration = 3.0f;
               float menuInitDelay = 2.0f;
               float fakeCoverDelay = 1.0f;

               var revealingScenario = new Scenario();

               revealingScenario.AddAct(() => _screenCoverer.FadeIn(blackCoverDuration));
               revealingScenario.AddDelay(menuInitDelay);

               revealingScenario.AddAct(() => new MainMenuPresenter(_screenCoverer));
               revealingScenario.AddDelay(fakeCoverDelay);

               revealingScenario.AddAct(() => _screenCoverer.FadeIn(fakeCoverDuration));
               revealingScenario.AddDelay(fakeCoverDuration * 0.5f);
               revealingScenario.AddAct(() => _screenCoverer.EnableUserInput());

               revealingScenario.Play(CoroutineOwner: this);
          }
     }
}
