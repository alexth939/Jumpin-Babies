using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;

namespace UnityEngine.UI
{
     [DisallowMultipleComponent]
     [RequireComponent(typeof(Image))]
     public class CoverImage: MonoBehaviour, ICoverImage, IBindable<ICoverImage>
     {
          [SerializeField] private Image _image;

          private AsyncBusyWorker _busyWorker = new AsyncBusyWorker();

          public ICoverImage Bind()
          {
               _image = GetComponent<Image>();
               return this;
          }

          void ICoverImage.CoverAsync(float duration, bool ignoreTimeScale, Action onCovered, Action onBusy)
          {
               _busyWorker.TryGetBusy(() =>
               {
                    _image.CrossFadeAlpha(Alpha.One, duration, ignoreTimeScale);
                    Thread.Sleep(new TimeSpan(0, 0, seconds: (int)duration));
               }, onCovered, onBusy);
          }

          void ICoverImage.UncoverAsync(float duration, bool ignoreTimeScale, Action onUncovered, Action onBusy)
          {
               _busyWorker.TryGetBusy(() =>
               {
                    _image.CrossFadeAlpha(Alpha.Zero, duration, ignoreTimeScale);
                    Thread.Sleep(new TimeSpan(0, 0, seconds: (int)duration));
               }, onUncovered, onBusy);
          }
     }
}
