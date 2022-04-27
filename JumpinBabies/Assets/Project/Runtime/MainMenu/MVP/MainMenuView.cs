using System;
using UnityEngine;
using UnityEngine.UI;

namespace JumpinBabies.MainMenu
{
     public class MainMenuView: MonoBehaviour, IMainMenuView, IBinder<IMainMenuView, IMainMenuPresenter>
     {
          [SerializeField] private GameObject _MainButtons;
          [SerializeField] private GameObject _OptionsButtons;
          [SerializeField] private MainMenuControls _controls;
          private IMainMenuPresenter _presenter;

          public IMainMenuView Bind(IMainMenuPresenter sender)
          {
               if(_presenter is not null)
                    throw new NotSupportedException("Already bound!");

               _presenter = sender;

               InitControlsSubscribers();
               SwitchToMainControls();
               RandomizeButtonsAnimationOffset();

               return this;
          }

          private void InitControlsSubscribers()
          {
               _controls.Play.onClick.AddListener(_presenter.StartGame);
               _controls.Exit.onClick.AddListener(_presenter.ExitGame);
               _controls.EnterOptions.onClick.AddListener(_presenter.TransitToOptions);

               _controls.ExitOptions.onClick.AddListener(_presenter.TransitToMain);
               _controls.Sound.onValueChanged.AddListener(_presenter.ToggleSound);
               _controls.Vibration.onValueChanged.AddListener(_presenter.ToggleVibration);
          }

          private void SwitchToMainControls()
          {
               _MainButtons.SetActive(true);
          }

          private void SwitchToOptionsControls()
          {

          }

          private void RandomizeButtonsAnimationOffset()
          {
               float[] randomOffset = new float[] { 1.21f, 2.23f, 3.25f };

               var playButton = _controls.Play;
               var enterOptionsButton = _controls.EnterOptions;
               var exitButton = _controls.Exit;

               Scenario randomizeScenario = new Scenario();

               randomizeScenario.AddAct(() => playButton.GetComponent<Animator>().speed += randomOffset[0]);
               randomizeScenario.AddAct(() => enterOptionsButton.GetComponent<Animator>().speed += randomOffset[1]);
               randomizeScenario.AddAct(() => exitButton.GetComponent<Animator>().speed += randomOffset[2]);

               randomizeScenario.AddDelay(0.3f);

               randomizeScenario.AddAct(() => playButton.GetComponent<Animator>().speed -= randomOffset[0]);
               randomizeScenario.AddAct(() => enterOptionsButton.GetComponent<Animator>().speed -= randomOffset[1]);
               randomizeScenario.AddAct(() => exitButton.GetComponent<Animator>().speed -= randomOffset[2]);

               randomizeScenario.Play(this);
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
