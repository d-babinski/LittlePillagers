using System;
using UnityEngine;

[CreateAssetMenu]
public class ResourcesVariable : ScriptableObject
{
    [SerializeField] private Resources value = new Resources();

    public Resources Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
