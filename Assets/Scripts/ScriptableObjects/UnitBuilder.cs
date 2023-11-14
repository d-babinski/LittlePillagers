using UnityEngine;

public abstract class UnitBuilder : ScriptableObject
{ 
    public abstract GameObject Build(UnitTemplate _template);
}
