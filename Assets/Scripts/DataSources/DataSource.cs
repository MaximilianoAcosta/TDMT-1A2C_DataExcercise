using UnityEngine;


public abstract class DataSource<T> : ScriptableObject
{
    [SerializeField] protected T _Value;
    public T Value 
    {
        get { return _Value; }
        set { _Value = value; }
    }
    
}