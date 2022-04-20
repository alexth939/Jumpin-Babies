using System;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameData
{
     /// <summary>
     /// Intended to Import, Export and Provide game progress to the client.
     /// </summary>
     sealed class GameProgressProvider: AsyncBusy
     {
          private const bool ExportOnModified = false;

          private static readonly Lazy<GameProgressProvider> _lazyInstance = new(() => new GameProgressProvider());

          private readonly DataTrader<BinaryFormatter> _dataTrader;
          private readonly string _path;

          private DataContainer<GameProgressModel> _progressContainer;

          private GameProgressProvider()
          {
               _path = Path.Combine(Application.persistentDataPath, "progress.dat");
               _dataTrader = new DataTrader<BinaryFormatter>();
          }

          public static GameProgressProvider SingleInstance { get => _lazyInstance.Value; }

          public void ExportAsync(Action onExported = default, Action onBusy = default)
          {
               TryGetBusy(() =>
               {
                    _dataTrader.ExportItem(_path, _progressContainer);
               }, onExported, onBusy);
          }

          public void ImportAsync(Action onImported = default, Action onBusy = default)
          {
               TryGetBusy(() =>
               {
                    if(File.Exists(_path))
                         _progressContainer = (DataContainer<GameProgressModel>)_dataTrader.ImportItem(_path);
                    else
                         _progressContainer = new();
               }, onImported, onBusy);
          }
     }
}
