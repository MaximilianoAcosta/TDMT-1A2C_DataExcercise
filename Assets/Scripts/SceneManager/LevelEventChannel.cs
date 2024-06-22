using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Sources/LevelEventChannel")]
public class LevelEventChannel : ScriptableObject
{
    public Action<Level> OnEventRaised;

    public void RaiseEvent(Level level)
    {
        if (OnEventRaised != null) OnEventRaised(level);
    }
}
