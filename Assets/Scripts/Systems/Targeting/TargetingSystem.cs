using System.Collections.Generic;
using UnityEngine;

public abstract class TargetingSystem : ScriptableObject
{
    public abstract Unit ChooseTarget(Vector3 _callerPosition, List<Unit> _unitsToChooseFrom);
}
