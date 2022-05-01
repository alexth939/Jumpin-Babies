using UnityEngine;
using JumpinBabies.MainMenu;

namespace JumpinBabies.SceneWorkers
{
     internal sealed class MainSceneWorker: SceneWorker
     {
          [SerializeField] private ScreenCoverer _screenCoverer;

          protected override void EnteringScene()
          {
               GenerateRevealMenuScenario().Play(CoroutineOwner: this);
          }

          private Scenario GenerateRevealMenuScenario()
          {
               Scenario scenario = new();

               scenario.AddAct(() => _screenCoverer.FadeIn(duration: 2.0f));
               scenario.AddDelay(duration: 2.0f);

               scenario.AddAct(() => new MainMenuPresenter(_screenCoverer));
               scenario.AddDelay(duration: 1.0f);

               scenario.AddAct(() => _screenCoverer.FadeIn(duration: 3.0f));
               scenario.AddDelay(duration: 1.5f);
               scenario.AddAct(() => _screenCoverer.EnableUserInput());

               return scenario;
          }
     }
}
