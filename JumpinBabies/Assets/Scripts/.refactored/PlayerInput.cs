using UnityEngine;

public class PlayerInput:MonoBehaviour
{
     [HideInInspector]
     public GameFlow gameFlowRef;

     void Start()
     {
          Input.multiTouchEnabled = false;
     }

     void Update()
     {
          if(Input.GetKeyDown(KeyCode.RightArrow))
               gameFlowRef.MovePlayerRight();

          if(Input.GetKeyDown(KeyCode.LeftArrow))
               gameFlowRef.MovePlayerLeft();

          if(Input.touches.Length == 1)
          {
               var _touch = Input.touches[0];
               if(_touch.phase == TouchPhase.Began)
               {
                    if(_touch.position.x < Screen.width / 2)
                         gameFlowRef.MovePlayerLeft();
                    else
                         gameFlowRef.MovePlayerRight();
               }
          }
     }
}