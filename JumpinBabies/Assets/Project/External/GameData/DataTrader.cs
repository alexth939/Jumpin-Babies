using System.IO;
using System.Runtime.Serialization;

namespace GameData
{
     // Summary:
     //   PlayerPrefs is Evil. dont use it on windows.
     public class DataTrader<FormatterType> where FormatterType : IFormatter, new()
     {
          /// <summary>
          /// Export with overwrite
          /// </summary>
          public void ExportItem(string fullPath, ISerializable dataContainer)
          {
               var formatter = new FormatterType();
               var outputStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);

               formatter.Serialize(outputStream, dataContainer);
               outputStream.Close();
          }

          public ISerializable ImportItem(string fullPath)
          {
               var formatter = new FormatterType();
               var inputStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.None);

               var instance = (ISerializable)formatter.Deserialize(inputStream);
               inputStream.Close();

               return instance;
          }
     }
}
