namespace System.Threading.Tasks
{
     //simple access locking mechanism
     public sealed class AsyncBusyWorker
     {
          public bool IsBusy { get; private set; }

          /// <summary>
          /// I'll try to invoke <paramref name="executeBody"/> if not busy.
          /// </summary>
          /// <param name="executeBody"></param>
          /// <param name="onDone">It wont be invoked if busy.</param>
          /// <param name="onBusy">It will be invoked instead.</param>
          public async void TryGetBusy(Action executeBody, Action onDone = default, Action onBusy = default)
          {
               if(CatchBusyException(onBusy))
                    return;

               IsBusy = true;
               await Task.Run(executeBody);
               IsBusy = false;

               onDone?.Invoke();
          }

          private bool CatchBusyException(Action onBusy)
          {
               if(IsBusy)
                    onBusy?.Invoke();
               return IsBusy;
          }
     }
}
