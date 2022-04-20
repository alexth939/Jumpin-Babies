using System;

namespace JumpinBabies.Common
{
     public class DelegateGenerator
     {
          //u can name them attempt#DoSomething#

          /// <summary>
          /// <para>Generates an Action that will invoke ur action after #delayCounter# amount of calls.</para>
          /// </summary>
          /// <param name="delayCounter">above zero please.</param>
          /// <param name="body">I'll invoke it when #delayCounter# equals 0.</param>
          /// <param name="disposeOnZero">set false, if for some reason u want to reuse me when #delayCounter# remains zero.</param>
          /// <returns>new generated Action.</returns>
          public static Action GenerateCounterDelayedAction(int delayCounter, Action body, bool disposeOnZero = true)
          {
               if(delayCounter <= 0)
                    throw new ArgumentOutOfRangeException(nameof(delayCounter));
               if(body == null)
                    throw new ArgumentNullException(nameof(body));

               return new Action(() =>
               {
                    if(delayCounter > 0)
                    {
                         delayCounter -= 1;
                    }
                    else
                    {
                         body.Invoke();

                         if(disposeOnZero)
                              body = null;
                    }
               });
          }

          /// <summary>
          /// <para>Generates a func that will invoke ur action after #delayCounter# amount of calls.</para>
          /// <para>And will return #delayCounter# that was last to call for invoke.</para>
          /// </summary>
          /// <param name="delayCounter">above zero please.</param>
          /// <param name="body">I'll invoke it when #delayCounter# equals 0.</param>
          /// <param name="disposeOnZero">set false, if for some reason u want to reuse me when #delayCounter# remains zero.</param>
          /// <returns>new generated func.</returns>
          public static Func<int> GenerateCounterDelayedFunc(int delayCounter, Action body, bool disposeOnZero = true)
          {
               if(delayCounter <= 0)
                    throw new ArgumentOutOfRangeException(nameof(delayCounter));
               if(body == null)
                    throw new ArgumentNullException(nameof(body));

               return new Func<int>(() =>
               {
                    if(delayCounter > 0)
                    {
                         delayCounter -= 1;

                         return delayCounter;
                    }
                    else
                    {
                         body.Invoke();

                         if(disposeOnZero)
                              body = null;

                         return 0;
                    }
               });
          }
     }
}
