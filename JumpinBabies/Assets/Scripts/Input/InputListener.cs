using System;
using System.Linq;
using UnityEngine;

sealed class InputListener: MonoBehaviour
{
     public bool Active { get; private set; }
     private Action _inputHandler;
     //private void Start()
     //{
     //     Bind((null, () => Debug.Log("asd")));
     //}
     public void Bind((Action left, Action right) moveDelegates)
     {
          new InputMethodsProvider(out _inputHandler, moveDelegates);
          Active = true;
     }
     public void Pause()
     {
          Active = false;
     }
     public void Continue()
     {
          using(Helpers helpers = new Helpers())
          {
               helpers.NullCheck(_inputHandler, Helpers.Importance.High);

               Active = true;
          }
     }

     private void Update()
     {
          if(Active == false)
               return;

          _inputHandler.Invoke();
     }

     private void OnApplicationQuit()
     {
          _inputHandler -= _inputHandler;
     }
}

sealed class InputMethodsProvider
{
     private Touch _singleTouch => Input.touches.Single();
     private Action _moveLeft;
     private Action _moveRight;

     public InputMethodsProvider(out Action inputDelegate, (Action left, Action right) moveDelegates)
     {
          using(Helpers helpers = new Helpers())
          {
               helpers.NullCheck(moveDelegates.left, Helpers.Importance.High);
               helpers.NullCheck(moveDelegates.right, Helpers.Importance.High);
          }

          _moveLeft = moveDelegates.left;
          _moveRight = moveDelegates.right;

#if UNITY_EDITOR
          inputDelegate = _pc;
#elif UNITY_STANDALONE
          inputDelegate = Android;
#endif
     }
     private Action _pc => () =>
     {
          if(Input.GetKeyDown(KeyCode.LeftArrow))
               _moveLeft();
          if(Input.GetKeyDown(KeyCode.RightArrow))
               _moveRight();
     };
     private Action _android => () =>
     {
          switch(Input.touches.Length)
          {
               case 1 when _singleTouch.phase == TouchPhase.Began:
               {
                    if(_singleTouch.position.x < Screen.width / 2)
                         _moveLeft();
                    else
                         _moveRight();
                    break;
               }
               //case 2:
               //{
               //     //pass pause command
               //     break;
               //}
          }
     };
     ~InputMethodsProvider()
     {
          _moveLeft -= _moveLeft;
          _moveRight -= _moveRight;
     }
}