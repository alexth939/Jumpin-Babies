using GameData;

namespace JumpinBabies.MainMenu
{
     public interface IMainMenuView
     {
          void SwitchToOptionsView(GameSettingsModel settings);
          void SwitchToMainView();
     }
}
