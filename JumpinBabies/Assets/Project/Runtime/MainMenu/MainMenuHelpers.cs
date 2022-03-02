using System;

namespace MainMenu
{
     public static class MainMenuHelpers
     {
          public static void EnsureStateIs(this MainMenuModel model, MainMenuState state)
          {
               if(model.State.Equals(state) == false)
               {
                    throw new NotSupportedException($"MainMenu has to be {Enum.GetName(typeof(MainMenuState), state)}!");
               }
          }

          public static void EnsureStateIsNot(this MainMenuModel model, MainMenuState state)
          {
               if(model.State.Equals(state) == true)
               {
                    throw new NotSupportedException($"MainMenu has to be not {Enum.GetName(typeof(MainMenuState), state)}!");
               }
          }
     }
}
