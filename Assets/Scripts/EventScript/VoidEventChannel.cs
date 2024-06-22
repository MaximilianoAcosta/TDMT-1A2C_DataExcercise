using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Sources/VoidEventChannel")]
public class VoidEventChannel : ScriptableObject
{
    public Action OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised != null) OnEventRaised();
    }
}
