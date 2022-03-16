using System;

sealed class InputListener: MonoBehaviour
{
     // isActiveAndEnabled can be used, but I dont want to mess with monobehaviours isActive flag
     public bool Active { get; private set; } = false;
     private Action _handleInput;

     //Entry Point:
     public void Bind(in (Action stepLeft, Action stepRight) moveDelegates, out (Action halt, Action listen) inputListenerControls, bool autoStart = true)
     {
          Helpers.ConfirmNotNull(moveDelegates.stepLeft, Helpers.Importance.High);
          Helpers.ConfirmNotNull(moveDelegates.stepRight, Helpers.Importance.High);
          Helpers.ConfirmNull(_handleInput, Helpers.Importance.Medium, "InputListener: better connect only one parent");

          //create instance if u planning to switch or reconfigure input method.
          new InputMethodProvider(moveDelegates, out _handleInput);
          inputListenerControls = (Halt, Listen);

          if(autoStart)
               Active = true;
     }

     #region MonoBehaviour
     private void Update()
     {
          if(Active)
               _handleInput();
     }
     private void OnDestroy()
     {
          _handleInput -= _handleInput;
     }
     #endregion

     private void Halt()
     {
          Active = false;
     }
     private void Listen()
     {
          Helpers.ConfirmNotNull(_handleInput, Helpers.Importance.High);

          Active = true;
     }
}
