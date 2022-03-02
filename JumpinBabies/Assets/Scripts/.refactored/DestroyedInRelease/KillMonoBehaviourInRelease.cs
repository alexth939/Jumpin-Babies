using UnityEngine;

/// <summary>
/// better exclude [developing helpers] at compilation. But I dk any way to achive it
/// </summary>
internal abstract class KillMonoBehaviourInRelease: MonoBehaviour
{
     private void Awake()
     {
          Destroy(this);
     }
}