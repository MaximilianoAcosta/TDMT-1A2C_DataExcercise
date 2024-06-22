using System;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Data/Sources/BoolEventChannel")]
public class BoolEventChannel : ScriptableObject
{
    public Action<bool> OnEventRaised;

    public void RaiseEvent(bool value)
    {
        if (OnEventRaised != null) OnEventRaised(value);
    }
}



