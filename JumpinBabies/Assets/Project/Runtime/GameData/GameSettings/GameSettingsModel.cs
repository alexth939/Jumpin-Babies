namespace GameData
{
     public class GameSettingsModel
     {
          public bool Sound;
          public bool Vibration;

          public object[] TestGetAllFieldsValues()
          {
               return new object[] { Sound, Vibration };
          }
     }
}