using System;

namespace UnityEngine.UI
{
     public interface ICoverImage
     {
          void CoverAsync(float duration, bool ignoreTimeScale = false, Action onCovered = default, Action onBusy = default);
          void UncoverAsync(float duration, bool ignoreTimeScale = false, Action onCovered = default, Action onBusy = default);
     }
}
