using System;
using System.Linq;
using UnityEngine;

sealed class InputMethodProvider
{
     private Touch _singleTouch => Input.touches.Single();
     private Action _stepLeft;
     private Action _stepRight;

     public InputMethodProvider(in (Action stepLeft, Action stepRight) moveDelegates, out Action inputMethod)
     {
          _stepLeft = moveDelegates.stepLeft;
          _stepRight = moveDelegates.stepRight;

#if UNITY_EDITOR
          inputMethod = PC0;
#elif UNITY_STANDALONE
          inputMethod = Android0;
#endif
     }

     //for inEditor tests only, so no need key bindings
     private void PC0()
     {
          if(Input.GetKeyDown(KeyCode.LeftArrow))
               _stepLeft();
          else if(Input.GetKeyDown(KeyCode.RightArrow))
               _stepRight();
     }
     private void Android0()
     {
          switch(Input.touches.Length)
          {
               case 1 when _singleTouch.phase == TouchPhase.Began:
               {
                    if(_singleTouch.position.x < Screen.width / 2)
                         _stepLeft();
                    else
                         _stepRight();
                    break;
               }
               //case 2:
               //{
               //     //pass pause command
               //     break;
               //}
          }
     }
     ~InputMethodProvider()
     {
          _stepLeft -= _stepLeft;
          _stepRight -= _stepRight;
     }
}
