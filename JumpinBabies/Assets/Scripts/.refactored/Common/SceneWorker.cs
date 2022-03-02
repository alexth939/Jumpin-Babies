namespace ProjectPact
{
     internal abstract class SceneWorker: MonoBehaviour
     {
          /// <summary>
          /// auto-invoked at base.Awake().
          /// </summary>
          protected abstract void SetupScene();
          /// <summary>
          /// finalize work here and implement next scene load.
          /// </summary>
          protected abstract void LeaveScene(ref string nextScene);
          /// <summary>
          /// dont forget to save and restore application state here. for low memory devices.
          /// </summary>
          protected abstract void OnApplicationFocus(bool focus);
          protected void Awake()
          {
               SetupScene();
          }
     }
}
