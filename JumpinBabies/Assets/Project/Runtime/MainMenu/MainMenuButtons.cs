using System;
using UnityEngine.UI;

namespace MainMenu
{
     [Serializable]
     public struct MainMenuButtons
     {
          public Button EnterOptionsBtn;
          public Button ExitOptionsBtn;
          public Toggle SoundToggle;
          public Toggle VibrationToggle;
          public Button PlayBtn;
          public Button ExitBtn;
     }
}
