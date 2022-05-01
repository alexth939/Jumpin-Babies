using UnityEngine;
using UnityEngine.SceneManagement;

namespace JumpinBabies.SceneWorkers
{
     [DisallowMultipleComponent]
     internal abstract class SceneWorker: MonoBehaviour
     {
          protected void Start()
          {
               EnteringScene();
          }

          protected void OnApplicationFocus(bool focus)
          {
               if(focus)
                    OnApplicationAcquiredFocus();
               else
                    OnApplicationLostFocus();
          }

          protected void SwitchScene(SceneName nextScene)
          {
               LeavingScene();
               SceneManager.LoadScene(nextScene.AsString(), LoadSceneMode.Single);
          }

          /// <summary>
          /// auto-invoked at base.Start().
          /// </summary>
          protected abstract void EnteringScene();
          protected virtual void OnApplicationAcquiredFocus() { }
          protected virtual void OnApplicationLostFocus() { }
          protected virtual void LeavingScene() { }
     }
}
