using ProjectPact;
using MainMenu;

sealed class MainSceneWorker: SceneWorker
{
     protected override void SetupScene()
     {
          new MainMenuPresenter().InitView();
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
