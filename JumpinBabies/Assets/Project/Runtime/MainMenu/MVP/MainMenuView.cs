using System;
using UnityEngine;
using UnityEngine.UI;

namespace JumpinBabies.MainMenu
{
     public class MainMenuView: MonoBehaviour, IMainMenuView, IBinder<IMainMenuView, IMainMenuPresenter>
     {
          [SerializeField] private GameObject _mainControls;
          [SerializeField] private GameObject _optionsControls;

          [SerializeField] private MainMenuControls _controlsCollection;
          private IMainMenuPresenter _presenter;

          public IMainMenuView Bind(IMainMenuPresenter sender)
          {
               if(_presenter is not null)
                    throw new NotSupportedException("Already bound!");

               _presenter = sender;

               InitControlsSubscribers();
               (this as IMainMenuView).SwitchToMainControls();
               RandomizeButtonsAnimationOffset();

               return this;
          }

          private void InitControlsSubscribers()
          {
               _controlsCollection.Play.onClick.AddListener(_presenter.StartGame);
               _controlsCollection.Exit.onClick.AddListener(_presenter.ExitGame);
               _controlsCollection.EnterOptions.onClick.AddListener(_presenter.TransitToOptions);

               _controlsCollection.ExitOptions.onClick.AddListener(_presenter.TransitToMain);
               _controlsCollection.Sound.onValueChanged.AddListener(_presenter.ToggleSound);
               _controlsCollection.Vibration.onValueChanged.AddListener(_presenter.ToggleVibration);
          }

          void IMainMenuView.SwitchToMainControls()
          {
               _mainControls.SetActive(true);
               _optionsControls.SetActive(false);
          }

          void IMainMenuView.SwitchToOptionsControls()
          {
               _optionsControls.SetActive(true);
               _mainControls.SetActive(false);
          }

          private void RandomizeButtonsAnimationOffset()
          {
               Scenario randomizeScenario = new Scenario();
               float[] randomOffsets = new float[] { 1.21f, 2.23f, 3.25f };
               float rewindDuration = 0.3f;

               var playButton = _controlsCollection.Play;
               var enterOptionsButton = _controlsCollection.EnterOptions;
               var exitButton = _controlsCollection.Exit;

               randomizeScenario.AddAct(() => playButton.GetComponent<Animator>().speed += randomOffsets[0]);
               randomizeScenario.AddAct(() => enterOptionsButton.GetComponent<Animator>().speed += randomOffsets[1]);
               randomizeScenario.AddAct(() => exitButton.GetComponent<Animator>().speed += randomOffsets[2]);

               randomizeScenario.AddDelay(rewindDuration);

               randomizeScenario.AddAct(() => playButton.GetComponent<Animator>().speed -= randomOffsets[0]);
               randomizeScenario.AddAct(() => enterOptionsButton.GetComponent<Animator>().speed -= randomOffsets[1]);
               randomizeScenario.AddAct(() => exitButton.GetComponent<Animator>().speed -= randomOffsets[2]);

               randomizeScenario.Play(CoroutineOwner: this);
          }

          private void OnDestroy()
          {
               _controlsCollection.EnterOptions.onClick.RemoveAllListeners();
               _controlsCollection.ExitOptions.onClick.RemoveAllListeners();
               _controlsCollection.Sound.onValueChanged.RemoveAllListeners();
               _controlsCollection.Vibration.onValueChanged.RemoveAllListeners();
               _controlsCollection.Play.onClick.RemoveAllListeners();
               _controlsCollection.Exit.onClick.RemoveAllListeners();
          }
     }
}
