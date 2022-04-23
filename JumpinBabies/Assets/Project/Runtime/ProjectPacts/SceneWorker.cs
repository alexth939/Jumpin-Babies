using UnityEngine;
using UnityEngine.SceneManagement;

namespace JumpinBabies.SceneWorkers
{
     public abstract class SceneWorker: MonoBehaviour
     {
          protected void Start()
          {
               SetupScene();
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
               PrepareLeaveScene();
               SceneManager.LoadScene(nextScene.AsString(), LoadSceneMode.Single);
          }

          /// <summary>
          /// auto-invoked at base.Start().
          /// </summary>
          protected abstract void SetupScene();
          protected virtual void OnApplicationAcquiredFocus() { }
          protected virtual void OnApplicationLostFocus() { }
          protected virtual void PrepareLeaveScene() { }
     }
}
