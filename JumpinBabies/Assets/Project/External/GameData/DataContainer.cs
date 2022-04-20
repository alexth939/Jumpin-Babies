using System.Runtime.Serialization;

namespace GameData
{
     public sealed class DataContainer<T>: ISerializable where T : new()
     {
          public readonly T _dataModel;

          /// <summary>
          /// default settings
          /// </summary>
          public DataContainer()
          {
               _dataModel = new T();
               //_dataModel.SetDefaults();
          }

          public DataContainer(T newSettings)
          {
               _dataModel = newSettings;
          }

          //import method will call this
          public DataContainer(SerializationInfo info, StreamingContext context)
          {
               var modelTypeFields = typeof(T).GetFields();

               foreach(var field in modelTypeFields)
               {
                    var fieldData = info.GetValue(field.Name, field.FieldType);
                    field.SetValue(_dataModel, fieldData);
               }
          }

          //export method will call this
          public void GetObjectData(SerializationInfo info, StreamingContext context)
          {
               var modelTypeFields = typeof(T).GetFields();

               foreach(var field in modelTypeFields)
               {
                    info.AddValue(field.Name, field.GetValue(_dataModel));
               }
          }
     }
}
