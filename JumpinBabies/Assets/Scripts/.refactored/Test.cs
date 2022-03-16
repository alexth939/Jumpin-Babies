using System;
using System.Collections.Generic;
using System.Threading.Tasks;

sealed class Test: ProjectPact.SceneWorker
{
     protected override void SetupScene()
     {
          throw new NotImplementedException();
     }
     protected override void LeaveScene(ref string nextScene)
     {
          throw new NotImplementedException();
     }

     protected override void OnApplicationFocus(bool focus)
     {
          throw new NotImplementedException();
     }
}

class DelegatePlayerSlim
{
     public bool IsPlaying { get; private set; }

     private Action _emptyDelegate => delegate { };
     private Queue<(Action method, float duration)> _animations;

     public DelegatePlayerSlim()
     {
          _animations = new();
     }

     /// <summary>
     /// FIFO. I don't care if some Action not finished
     /// </summary>
     public async void Play()
     {
          ConfirmNotBusy();

          IsPlaying = true;
          {
               while(_animations.TryDequeue(out var animation))
               {
                    _ = Task.Run(animation.method);
                    await Task.Delay((int)(animation.duration * 1000));
               }
          }
          IsPlaying = false;
     }
     public DelegatePlayerSlim QueueAnimation(Action method, float durationSex = default)
     {
          if(method is null)
               throw new Exception("missing delegate!");

          ConfirmNotBusy();

          _animations.Enqueue((method, durationSex));

          return this;
     }
     public DelegatePlayerSlim QueueDelay(float seconds)
     {
          ConfirmNotBusy();
          _animations.Enqueue((_emptyDelegate, seconds));
          return this;
     }

     private void ConfirmNotBusy()
     {
          if(IsPlaying)
               throw new Exception("leave me alone! I'm busy. use tha IsPlaying flag");
     }
}

//under development
class DelegatePlayer
{
     // usage example:
     //     //good programmer
     //     player.QueueAnimation(isCancelRequested =>
     //          {
     //               do
     //               {
     //                    Debug.Log($"doing some important work");
     //               } while(isCancelRequested() == false) ;
     //          }).
     //          Play();

     ////bad bad programmer!
     //player.QueueAnimation(_ =>
     //{
     //     do
     //     {
     //          Debug.Log($"my mama says i'm smart boy");
     //     } while(true);
     //}).
     //Play();

     public delegate void CancelableMethod(Func<bool> isCancelRequested);
     public bool IsPlaying { get; private set; }

     //private Action _emptyDelegate => () => { };
     private CancelableMethod _emptyDelegate => _ => { };
     //private Queue<(Action method, float duration)> _animations;
     private Queue<(CancelableMethod method, float duration)> _animations;
     private List<Task> _startedTasks;
     private bool _cancelPlayRequired;

     public DelegatePlayer()
     {
          _animations = new();
     }
     /// <summary>
     /// LIFO
     /// </summary>
     public DelegatePlayer QueueAnimation(CancelableMethod method, float durationSex = default)
     {
          if(method is null)
               throw new Exception("missing delegate!");

          ConfirmNotBusy();

          _animations.Enqueue((method, durationSex));

          return this;
     }
     public DelegatePlayer QueueDelay(float seconds)
     {
          ConfirmNotBusy();
          _animations.Enqueue((_emptyDelegate, seconds));
          return this;
     }
     /// <summary>
     /// I don't care if some Action not finished
     /// </summary>
     public async void Play()
     {
          ConfirmNotBusy();

          _startedTasks = new List<Task>();

          //Queue<(Action del, float duration)> animationsCopy = new (_animations);
          IsPlaying = true;
          {
               while(_animations.TryDequeue(out var animation))
               {
                    _ = Task.Run(() => animation.method(() => _cancelPlayRequired));
                    await Task.Delay((int)(animation.duration * 1000));
               }
          }
          IsPlaying = false;
     }

     private void ConfirmNotBusy()
     {
          if(IsPlaying)
               throw new Exception("leave me alone! I'm busy. use tha IsPlaying flag");
     }

     ~DelegatePlayer()
     {
          //_ct.
     }
}
