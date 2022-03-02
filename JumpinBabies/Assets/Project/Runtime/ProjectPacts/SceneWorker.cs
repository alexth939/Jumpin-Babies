using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectPact
{
     public abstract class SceneWorker: MonoBehaviour
     {
          /// <summary>
          /// auto-invoked at base.Start().
          /// </summary>
          protected abstract void SetupScene();
          protected abstract void OnApplicationAcquiredFocus();
          protected abstract void OnApplicationLostFocus();

          protected void SwitchScene(SceneName nextScene)
          {
               PrepareLeaveScene();
               SceneManager.LoadScene(nextScene.AsString(), LoadSceneMode.Single);
          }
          protected abstract void PrepareLeaveScene();

          protected void OnApplicationFocus(bool focus)
          {
               if(focus)
                    OnApplicationAcquiredFocus();
               else
                    OnApplicationLostFocus();
          }
          protected void Start()
          {
               SetupScene();
          }
     }
}
