using System;
using UnityEngine.UI;

namespace JumpinBabies.MainMenu
{
     [Serializable]
     public struct MainMenuControls
     {
          public Button EnterOptions;
          public Button ExitOptions;
          public Toggle Sound;
          public Toggle Vibration;
          public Button Play;
          public Button Exit;
     }
}
