using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Targeting/GetClosestEnemy")]
public class GetClosestEnemy : TargetingSystem
{
    public override Unit ChooseTarget(Unit _caller, List<Unit> _allUnits)
    {
        List<Unit> _enemies = _allUnits.FindAll(_unit => _unit.UnitTeam != _caller.UnitTeam);
        
        if (_enemies.Count <= 0)
        {
            return null;
        }

        Unit _closestEnemy = null;
        float _distanceToBeat = Mathf.Infinity;
        
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].CurrentState == Unit.UnitState.Dead)
            {
                continue;
            }
            
            float _distance =  Vector2.Distance(_caller.transform.position, _enemies[i].transform.position);

            if (_distanceToBeat < _distance)
            {
                continue;
            }

            _distanceToBeat = _distance;
            _closestEnemy = _enemies[i];
        }

        return _closestEnemy;
    }
}
