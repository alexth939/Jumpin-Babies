using JumpinBabies.MainMenu;

namespace JumpinBabies.SceneWorkers
{
     sealed class MainSceneWorker: SceneWorker
     {
          protected override void SetupScene()
          {
               new MainMenuPresenter().InitView();
          }
     }
}
