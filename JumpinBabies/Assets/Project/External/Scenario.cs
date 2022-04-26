using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
     public class Scenario
     {
          private readonly List<(ActType type, object value)> _scenario;

          public Scenario() => _scenario = new();

          private enum ActType
          {
               Action,
               Delay,
               ProceedTrigger
          }

          public void AddAct(Action act) => _scenario.Add((ActType.Action, act));

          public void AddDelay(float duration) => _scenario.Add((ActType.Delay, duration));

          public void AddProceedTrigger(Func<bool> proceedTrigger) => _scenario.Add((ActType.ProceedTrigger, proceedTrigger));

          public void Play(MonoBehaviour CoroutineOwner) => CoroutineOwner.StartCoroutine(PlayScenario());

          IEnumerator PlayScenario()
          {
               foreach(var Act in _scenario)
               {
                    switch(Act.type)
                    {
                         case ActType.Action:
                         {
                              (Act.value as Action).Invoke();
                              yield return null;
                              break;
                         }
                         case ActType.Delay:
                         {
                              yield return new WaitForSeconds((float)Act.value);
                              break;
                         }
                         case ActType.ProceedTrigger:
                         {
                              yield return new WaitUntil(Act.value as Func<bool>);
                              break;
                         }
                    }
               }
          }
     }
}
