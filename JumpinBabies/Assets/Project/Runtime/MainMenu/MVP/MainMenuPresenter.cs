using System;
using GameData;
using UnityEngine;

namespace JumpinBabies.MainMenu
{
     public sealed class MainMenuPresenter: IMainMenuPresenter
     {
          private readonly IMainMenuView _view;
          private readonly MainMenuModel _model;
          private readonly GameSettingsModel _settingsTemplate;
          private readonly GameSettingsProvider _settingsProvider;
          private readonly ScreenCoverer _screenCoverer;
          private float _crossFadeDuration = 0.5f;

          public MainMenuPresenter(ScreenCoverer covererInstance)
          {
               _model = new MainMenuModel();
               BindToView(out _view);
               _settingsProvider = GameSettingsProvider.SingleInstance;
               _screenCoverer = covererInstance;
#warning for release:
               //_settingsTemplate = _settingsProvider.AllSettings;
               _settingsTemplate = new GameSettingsModel() { Sound = false, Vibration = true };
          }

          private void BindToView(out IMainMenuView view)
          {
               view = MoreObject.FindSingleSceneObject<MainMenuView>().Bind(this);
          }

          // todo expand method:
          //   1. play circle anim
          //   2. implement hide on done
          private void ApplySettings() => _settingsProvider.ApplyAsync(_settingsTemplate);

          void IMainMenuPresenter.StartGame()
          {
               throw new NotImplementedException();
          }

          void IMainMenuPresenter.ExitGame()
          {
               throw new NotImplementedException();
          }

          void IMainMenuPresenter.ToggleSound(bool value) => _settingsTemplate.Sound = value;
          void IMainMenuPresenter.ToggleVibration(bool value) => _settingsTemplate.Vibration = value;

          void IMainMenuPresenter.TransitToOptions()
          {
               GenerateMainToOptionsScenario().Play(CoroutineOwner: _screenCoverer);
          }

          void IMainMenuPresenter.TransitToMain()
          {
               GenerateOptionsToMainScenario().Play(CoroutineOwner: _screenCoverer);
          }

          private Scenario GenerateMainToOptionsScenario()
          {
               var scenario = new Scenario();
               float fadeHalfDuration = _crossFadeDuration * 0.5f;

               scenario.AddAct(_screenCoverer.DisableUserInput);
               scenario.AddAct(() => _screenCoverer.FadeOut(_crossFadeDuration));
               scenario.AddDelay(_crossFadeDuration);

               scenario.AddAct(()=>_view.SwitchToOptionsView(_settingsTemplate));
               scenario.AddAct(() => _screenCoverer.FadeIn(_crossFadeDuration));
               scenario.AddDelay(fadeHalfDuration);

               scenario.AddAct(_screenCoverer.EnableUserInput);

               return scenario;
          }

          private Scenario GenerateOptionsToMainScenario()
          {
               var scenario = new Scenario();
               float fadeHalfDuration = _crossFadeDuration * 0.5f;

               scenario.AddAct(_screenCoverer.DisableUserInput);
               scenario.AddAct(() => _screenCoverer.FadeOut(_crossFadeDuration));
               scenario.AddDelay(_crossFadeDuration);

               // todo 1. start saving anim
               // todo 2. save GameSettings
               // todo 3. delay 1? sec (to play the animation at least for 1? sec)
               // todo 4. wait for saveDone

               scenario.AddAct(_view.SwitchToMainView);
               scenario.AddAct(() => _screenCoverer.FadeIn(_crossFadeDuration));
               scenario.AddDelay(fadeHalfDuration);

               scenario.AddAct(_screenCoverer.EnableUserInput);

               return scenario;
          }
     }
}
