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
               Scenario buttonsScenario = new Scenario();
               float buttonAnimationOffsetDifference = 0.5f;
               var playButton = _controls.Play;
               var enterOptionsButton = _controls.EnterOptions;
               var exitButton = _controls.Exit;

               //buttonsScenario.AddAct(() => _buttons.PlayBtn.GetComponent<Animator>().enabled = true);
               buttonsScenario.AddAct(() => playButton.GetComponent<Animator>().SetTrigger("StartTrigger"));
               buttonsScenario.AddDelay(buttonAnimationOffsetDifference);

               //buttonsScenario.AddAct(() => _buttons.EnterOptionsBtn.GetComponent<Animator>().enabled = true);
               buttonsScenario.AddAct(() => enterOptionsButton.GetComponent<Animator>().SetTrigger("StartTrigger"));
               buttonsScenario.AddDelay(buttonAnimationOffsetDifference);
               //buttonsScenario.AddAct(() => _buttons.ExitBtn.GetComponent<Animator>().enabled = true);
               buttonsScenario.AddAct(() => exitButton.GetComponent<Animator>().SetTrigger("StartTrigger"));

               buttonsScenario.Play(CoroutineOwner: this);
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
