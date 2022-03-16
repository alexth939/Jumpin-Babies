using UnityEngine;

internal class Note: KillMonoBehaviourInRelease
{
     [TextArea(1, 10)]
     [SerializeField] private string _text;
}