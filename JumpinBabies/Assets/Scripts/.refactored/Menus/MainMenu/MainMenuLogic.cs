using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic
{
     private MenuState _state;
     private enum MenuState
     {
          MainMenu,
          Options,
          Transitting
     }
}
