namespace JumpinBabies.MainMenu
{
     public interface IMainMenuPresenter
     {
          void TransitToOptions();
          void TransitToMain();
          void ToggleSound(bool value);
          void ToggleVibration(bool value);
          void StartGame();
          void ExitGame();
     }
}
