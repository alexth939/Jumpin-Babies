using System;
using UnityEngine;

namespace MainMenu
{
     public class MainMenuView: MonoBehaviour, IMainMenuView, IBinder<IMainMenuView, IMainMenuPresenter>
     {
          [SerializeField] private MainMenuButtons _buttons;
          private IMainMenuPresenter _presenter;

          public IMainMenuView Bind(IMainMenuPresenter sender)
          {
               if(_presenter is not null)
                    throw new NotSupportedException("Already bound!");

               _presenter = sender;

               _buttons.EnterOptionsBtn.onClick.AddListener(_presenter.TransitToSettings);
               _buttons.ExitOptionsBtn.onClick.AddListener(_presenter.TransitToMain);
               _buttons.SoundToggle.onValueChanged.AddListener(_presenter.ToggleSound);
               _buttons.VibrationToggle.onValueChanged.AddListener(_presenter.ToggleVibration);
               _buttons.PlayBtn.onClick.AddListener(_presenter.StartGame);
               _buttons.ExitBtn.onClick.AddListener(_presenter.ExitGame);

               Debug.Log($"MainMenuView: Successfully Bound to presenter ;)");

               return this;
          }

          private void OnDestroy()
          {
               _buttons.EnterOptionsBtn.onClick.RemoveAllListeners();
               _buttons.ExitOptionsBtn.onClick.RemoveAllListeners();
               _buttons.SoundToggle.onValueChanged.RemoveAllListeners();
               _buttons.VibrationToggle.onValueChanged.RemoveAllListeners();
               _buttons.PlayBtn.onClick.RemoveAllListeners();
               _buttons.ExitBtn.onClick.RemoveAllListeners();
          }
     }
}
