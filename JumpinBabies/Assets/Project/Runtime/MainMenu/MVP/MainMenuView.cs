using System;
using UnityEngine;
using GameData;

namespace JumpinBabies.MainMenu
{
     public partial class MainMenuView: MonoBehaviour, IMainMenuView, IBinder<IMainMenuView, IMainMenuPresenter>
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
               (this as IMainMenuView).SwitchToMainView();
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

          void IMainMenuView.SwitchToMainView()
          {
               _mainControls.SetActive(true);
               _optionsControls.SetActive(false);
          }

          void IMainMenuView.SwitchToOptionsView(GameSettingsModel settings)
          {
               _optionsControls.SetActive(true);
               _mainControls.SetActive(false);
               FillOptionsParameters(settings);
          }

          private void FillOptionsParameters(GameSettingsModel settings)
          {
               _controlsCollection.Sound.isOn = settings.Sound;
               _controlsCollection.Vibration.isOn = settings.Vibration;
          }

          private void RandomizeButtonsAnimationOffset()
          {
               GenerateRandomizeButtonsScenario().Play(CoroutineOwner: this);
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

     public partial class MainMenuView: MonoBehaviour, IMainMenuView, IBinder<IMainMenuView, IMainMenuPresenter>
     {
          private Scenario GenerateRandomizeButtonsScenario()
          {
               Scenario scenario = new();

               float[] randomOffsets = new float[] { 1.21f, 2.23f, 3.25f };
               float rewindDuration = 0.3f;

               var playButtonAnimator = _controlsCollection.Play.GetComponent<Animator>();
               var enterOptionsButtonAnimator = _controlsCollection.EnterOptions.GetComponent<Animator>();
               var exitButtonAnimator = _controlsCollection.Exit.GetComponent<Animator>();

               scenario.AddAct(() => playButtonAnimator.speed += randomOffsets[0]);
               scenario.AddAct(() => enterOptionsButtonAnimator.speed += randomOffsets[1]);
               scenario.AddAct(() => exitButtonAnimator.speed += randomOffsets[2]);

               scenario.AddDelay(rewindDuration);

               scenario.AddAct(() => playButtonAnimator.speed -= randomOffsets[0]);
               scenario.AddAct(() => enterOptionsButtonAnimator.speed -= randomOffsets[1]);
               scenario.AddAct(() => exitButtonAnimator.speed -= randomOffsets[2]);

               return scenario;
          }
     }
}
