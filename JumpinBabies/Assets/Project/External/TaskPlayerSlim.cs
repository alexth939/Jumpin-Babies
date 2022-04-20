using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TaskPlayerSlim: IDisposable
{
     public bool IsPlaying { get; private set; }

     private Action _emptyDelegate => delegate { };
     private Queue<(Action method, float duration)> _animations;

     public TaskPlayerSlim()
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
     public TaskPlayerSlim QueueAnimation(Action method, float durationSex = default)
     {
          if(method is null)
               throw new Exception("missing delegate!");

          ConfirmNotBusy();

          _animations.Enqueue((method, durationSex));

          return this;
     }
     public TaskPlayerSlim QueueDelay(float seconds)
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

     public void Dispose()
     {
          _animations.Clear();
     }
}
