using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// I assume that u don't manually modify GameObjects in container at runtime.
/// </summary>
[DisallowMultipleComponent]
public class ScreenCoverer: MonoBehaviour
{
     private const bool IgnoreTimeScale = false;

     private int _lastVisibleCoverIndex;
     private Image[] _covers;

     private void Awake() => GetCoversIfNull();

     private void GetCoversIfNull()
     {
          if(_covers is not null)
               return;

          var attachedTransform = transform;
          int childCount = attachedTransform.childCount;

          _covers = new Image[childCount];

          for(int i = 0; i < childCount; i++)
          {
               var child = attachedTransform.GetChild(i).GetComponent<Image>();
               _covers[i] = child;
          }

          _lastVisibleCoverIndex = childCount - 1;
     }

     public void FadeOut(float duration)
     {
          GetCoversIfNull();
          _covers[++_lastVisibleCoverIndex].CrossFadeAlpha(Alpha.One, duration, IgnoreTimeScale);
     }

     public void FadeIn(float duration)
     {
          GetCoversIfNull();
          _covers[_lastVisibleCoverIndex--].CrossFadeAlpha(Alpha.Zero, duration, IgnoreTimeScale);
     }
}
