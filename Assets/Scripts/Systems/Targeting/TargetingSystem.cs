using System.Collections.Generic;
using UnityEngine;

public abstract class TargetingSystem : ScriptableObject
{
    public abstract Unit ChooseTarget(Unit _callingUnit, UnitRuntimeSet _allUnits);
}
