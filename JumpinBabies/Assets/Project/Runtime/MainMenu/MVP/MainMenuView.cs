using System;
using UnityEngine;
using UnityEngine.UI;

namespace JumpinBabies.MainMenu
{
     public class MainMenuView: MonoBehaviour, IMainMenuView, IBinder<IMainMenuView, IMainMenuPresenter>
     {
          [SerializeField] private MainMenuControls _controls;
          private IMainMenuPresenter _presenter;

          public IMainMenuView Bind(IMainMenuPresenter sender)
          {
               if(_presenter is not null)
                    throw new NotSupportedException("Already bound!");

               _presenter = sender;

               InitControlsSubscribers();
               InitControlsAnimation();

               return this;
          }

          private void InitControlsSubscribers()
          {
               _controls.EnterOptions.onClick.AddListener(_presenter.TransitToSettings);
               _controls.ExitOptions.onClick.AddListener(_presenter.TransitToMain);
               _controls.Sound.onValueChanged.AddListener(_presenter.ToggleSound);

               _controls.Vibration.onValueChanged.AddListener(_presenter.ToggleVibration);
               _controls.Play.onClick.AddListener(_presenter.StartGame);
               _controls.Exit.onClick.AddListener(_presenter.ExitGame);
          }

          private void InitControlsAnimation()
          {
               float[] randomOffset = new float[] { 0.21f, 0.23f, 0.25f };

               var playButton = _controls.Play;
               var enterOptionsButton = _controls.EnterOptions;
               var exitButton = _controls.Exit;

               playButton.GetComponent<Animator>().speed += randomOffset[0];
               enterOptionsButton.GetComponent<Animator>().speed += randomOffset[1];
               exitButton.GetComponent<Animator>().speed += randomOffset[2];
          }

          private void OnDestroy()
          {
               _controls.EnterOptions.onClick.RemoveAllListeners();
               _controls.ExitOptions.onClick.RemoveAllListeners();
               _controls.Sound.onValueChanged.RemoveAllListeners();
               _controls.Vibration.onValueChanged.RemoveAllListeners();
               _controls.Play.onClick.RemoveAllListeners();
               _controls.Exit.onClick.RemoveAllListeners();
          }
     }
}
