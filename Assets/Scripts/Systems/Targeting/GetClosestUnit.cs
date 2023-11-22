using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Targeting/GetClosestUnit")]
public class GetClosestUnit : TargetingSystem
{
    public override Unit ChooseTarget(Vector3 _callerPosition, List<Unit> _unitsToChooseFrom)
    {
        if (_unitsToChooseFrom.Count <= 0)
        {
            return null;
        }

        Unit _closestUnit = _unitsToChooseFrom[0];
        float _distanceToBeat = Vector2.Distance(_callerPosition, _unitsToChooseFrom[0].transform.position);
        
        for (int i = 1; i < _unitsToChooseFrom.Count; i++)
        {
            float _calcualtedDistance =  Vector2.Distance(_callerPosition, _unitsToChooseFrom[i].transform.position);

            if (_distanceToBeat < _calcualtedDistance)
            {
                continue;
            }

            _distanceToBeat = _calcualtedDistance;
            _closestUnit = _unitsToChooseFrom[i];
        }

        return _closestUnit;
    }
}
