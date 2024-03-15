using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Targeting/GetClosestUnit")]
public class GetClosestUnit : TargetingSystem
{
    
    
    public override Unit ChooseTarget(Unit _callerPosition, UnitRuntimeSet _allUnits)
    {
        if (_allUnits.Items.Count <= 0)
        {
            return null;
        }

        Unit _closestUnit = _allUnits.Items[0];
        float _distanceToBeat = Vector2.Distance(_callerPosition.transform.position, _allUnits.Items[0].transform.position);
        
        for (int i = 1; i < _allUnits.Items.Count; i++)
        {
            float _calcualtedDistance =  Vector2.Distance(_callerPosition.transform.position, _allUnits.Items[i].transform.position);

            if (_distanceToBeat < _calcualtedDistance)
            {
                continue;
            }

            _distanceToBeat = _calcualtedDistance;
            _closestUnit = _allUnits.Items[i];
        }

        return _closestUnit;
    }
}
