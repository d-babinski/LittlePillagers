using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class UnitsAI : ScriptableObject
{
    public void UpdateUnitCombatAI(List<Unit> _allUnits)
    {
        _allUnits.ForEach(_unit =>
        {
            Vector2 _unitPos = _unit.transform.position;

            switch (_unit.CurrentState)
            {
                case Unit.UnitState.Idle:
                    _unit.CurrentAttack = selectNewAttack(_unit, _allUnits);

                    if (_unit.CurrentAttack == null)
                    {
                        return;
                    }
                    
                    _unit.Target = getBestTargetForAttack(_unit, _unit.CurrentAttack, _allUnits);

                    if (_unit.CurrentAttack.IsInRange(_unitPos, _unit.Target.transform.position))
                    {
                        _unit.ChangeState(Unit.UnitState.Attack);
                        return;
                    }

                    _unit.ChangeState(Unit.UnitState.Move);
                    break;

                case Unit.UnitState.Move:
                    Vector2 _targetPos = _unit.Target.transform.position;

                    if (_unit.CurrentAttack.IsTooClose(_unitPos, _targetPos))
                    {
                        Vector2 _destinationAwayFromTarget = (_targetPos - _unitPos).normalized * _unit.CurrentAttack.MinRange;
                        _unit.MoveTo(_destinationAwayFromTarget);
                        return;
                    }

                    if (_unit.CurrentAttack.IsTooFar(_unitPos, _targetPos))
                    {
                        _unit.MoveTo(_targetPos);
                        return;
                    }   

                    _unit.ChangeState(Unit.UnitState.Attack);
                    break;

                case Unit.UnitState.Attack:
                    if (_unit.CurrentAttackProgress >= 1f)
                    {
                        _unit.ChangeState(Unit.UnitState.Idle);
                        return;
                    }

                    _unit.ProgressCurrentAttack();
                    break;

                case Unit.UnitState.Dead:
                    return;
            }
        });
    }

    private Unit getBestTargetForAttack(Unit _unit, Attack _attack, List<Unit> _allUnits)
    {
        return _attack.GetTarget(_unit, _allUnits);
    }

    private Attack selectNewAttack(Unit _unit, List<Unit> _allUnits)
    {
        UnitType _unitType = _unit.UnitType;
        System.Random _rng = new System.Random();
        int[] _randomOrder = Enumerable.Range(0, _unitType.Attacks.Length).OrderBy((_item) => _rng.Next()).ToArray();

        for (int i = 0; i < _randomOrder.Length; i++)
        {
            int _attackId = _randomOrder[i];
            
            if (_unitType.Attacks[_attackId].IsOffCooldown(_unit.LastUseOfAttacks[_unitType.Attacks[_attackId]]) == false)
            {
                continue;
            }
            
            Unit _attackTarget = _unitType.Attacks[_attackId].GetTarget(_unit, _allUnits);

            if (_attackTarget != null)
            {
                return _unitType.Attacks[_attackId];
            }
        }

        return null;
    }
}
