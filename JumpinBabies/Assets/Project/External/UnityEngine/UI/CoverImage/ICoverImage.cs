using System;

public interface ICoverImage
{
     void CoverAsync(float duration, bool ignoreTimeScale = false, Action onCovered = default, Action onBusy = default);
     void UncoverAsync(float duration, bool ignoreTimeScale = false, Action onCovered = default, Action onBusy = default);
}
