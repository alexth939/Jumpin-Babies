using System;
using MoreUnityEngine;
using UnityEngine.Events;
using GameData;

namespace JumpinBabies.MainMenu
{
     public sealed class MainMenuPresenter: IMainMenuPresenter
     {
          private readonly MainMenuView _view;
          private readonly MainMenuModel _model;
          private readonly GameSettingsModel _settingsTemplate;
          private readonly GameSettingsProvider _settingsProvider;

          public MainMenuPresenter()
          {
               _model = new MainMenuModel();
               BindToView(out _view);
               _settingsProvider = GameSettingsProvider.SingleInstance;
               _settingsTemplate = _settingsProvider.AllSettings;
          }

          public void InitView()
          {
               _model.EnsureStateIs(MainMenuState.Covered);

               // 1. reveal the menu
               // 2. fill the settings in view
               // 3. RandomizeBtnsAnimOffset

               using(var player = new TaskPlayerSlim())
               {
                    //player.QueueAnimation(reveal).
                    //       QueueAnimation(fillSettings ?).
                    //       QueueAnimation(randomize).
                    //       Play();
               }
          }

          private void BindToView(out MainMenuView view)
          {
               view = MoreObject.FindSingleSceneObject<MainMenuView>();
               view.Bind(this);
          }

          // todo expand method:
          //   1. play circle anim
          //   2. implement hide on done
          private void ApplySettings() => _settingsProvider.ApplyAsync(_settingsTemplate);

          void IMainMenuPresenter.ExitGame()
          {
               throw new NotImplementedException();
          }

          void IMainMenuPresenter.StartGame()
          {
               throw new NotImplementedException();
          }

          void IMainMenuPresenter.ToggleSound(bool value) => _settingsTemplate.Sound = value;
          void IMainMenuPresenter.ToggleVibration(bool value) => _settingsTemplate.Vibration = value;

          void IMainMenuPresenter.TransitToMain()
          {
               // todo implement:
               //   1. dim screen
               //   2. apply
               //   2.1. pass delayed code to brighten screen
               throw new NotImplementedException();
          }

          void IMainMenuPresenter.TransitToSettings()
          {
               throw new NotImplementedException();
          }
     }
}
