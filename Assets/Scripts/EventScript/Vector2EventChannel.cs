using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Sources/Vector2EventChannel")]
public class Vector2EventChannel : ScriptableObject
{
    public Action<Vector2> OnEventRaised;

    public void RaiseEvent(Vector2 vector2)
    {
        if (OnEventRaised != null) OnEventRaised(vector2);
    }
}
