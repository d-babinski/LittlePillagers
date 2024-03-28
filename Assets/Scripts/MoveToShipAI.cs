using System.Collections.Generic;
using UnityEngine;

public static class MoveToShipAI
{
    public enum State
    {
        Moving = 0,
        AllUnitsMoved = 1,
    }

    public static State MoveAliveUnitsToPointAndDestroy(List<Unit> _units, Vector2 _target)
    {
        State _returnedState = State.AllUnitsMoved;
        
        _units.ForEach(_unit =>
        {
            if (_unit == null)
            {
                return;
            }
            
            if (_unit.CurrentState == Unit.UnitState.Dead)
            {
                MonoBehaviour.Destroy(_unit.gameObject);
                return;
            }

            _returnedState = State.Moving;

            if (_unit.CurrentState != Unit.UnitState.Move)
            {
                _unit.ChangeState(Unit.UnitState.Move);
            }

            if (Vector2.Distance(_target, _unit.transform.position) <= Mathf.Epsilon)
            {
                MonoBehaviour.Destroy(_unit.gameObject);
                return;
            }
            
            _unit.Face(_target.x < _unit.transform.position.x ? Unit.FaceDirection.Left : Unit.FaceDirection.Right);
            _unit.MoveTo(_target);
        });

        return _returnedState;
    }
}
