using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace GameData
{
     public sealed class GameSettingsProvider: AsyncBusy
     {
          private static readonly Lazy<GameSettingsProvider> _lazyInstance = new(() => new GameSettingsProvider());

          private readonly DataTrader<BinaryFormatter> _dataTrader;
          private readonly string _path;

          private DataContainer<GameSettingsModel> _settingsContainer;

          private GameSettingsProvider()
          {
               _path = Path.Combine(Application.persistentDataPath, "settings.dat");
               _dataTrader = new DataTrader<BinaryFormatter>();
          }

          public event Action OnApplyRequested;

          public static GameSettingsProvider SingleInstance { get => _lazyInstance.Value; }
          public GameSettingsModel AllSettings { get => _settingsContainer._dataModel; }

          public void ApplyAsync(Action onApplied = default, Action onBusy = default)
          {
               TryGetBusy(() =>
               {
                    SingleInstance.OnApplyRequested?.Invoke();
               }, onApplied, onBusy);
          }

          public void ApplyAsync(GameSettingsModel newSettings, Action onApplied = default, Action onBusy = default)
          {
               TryGetBusy(() =>
               {
                    _settingsContainer = new DataContainer<GameSettingsModel>(newSettings);
                    SingleInstance.OnApplyRequested?.Invoke();
               }, onApplied, onBusy);
          }

          public void ExportAsync(Action onExported = default, Action onBusy = default)
          {
               TryGetBusy(() =>
               {
                    _dataTrader.ExportItem(_path, _settingsContainer);
               }, onExported, onBusy);
          }

          public void ImportAsync(Action onImported = default, Action onBusy = default)
          {
               TryGetBusy(() =>
               {
                    if(File.Exists(_path))
                         _settingsContainer = (DataContainer<GameSettingsModel>)_dataTrader.ImportItem(_path);
                    else
                         _settingsContainer = new();
               }, onImported, onBusy);
          }
     }
}
