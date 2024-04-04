using System.Collections.Generic;

public class Battle
{
    public enum State
    {
        InProgress = 0,
        AttackerWon = 1,
        DefenderWon = 2,
    }
    
    public State BattleState => currentBattleState;

    public List<Unit> AttackerUnits => attackerUnits;
    private Army attackerArmy = null;
    private Army defenderArmy = null;
    private List<Unit> attackerUnits = new();
    private List<Unit> defenderUnits = new();
    private List<Unit> allUnits = new();
    private State currentBattleState = State.InProgress;
        
    public Battle(Army _attackerArmy, Army _defenderArmy, List<Unit> _attackerUnits, List<Unit> _defenderUnits)
    {
        attackerArmy = _attackerArmy;
        defenderArmy = _defenderArmy;
        attackerUnits = _attackerUnits;
        defenderUnits = _defenderUnits;
        
        allUnits.AddRange(_attackerUnits);
        allUnits.AddRange(_defenderUnits);
        
        SubscribeToUnitsDeathEventAndRemoveFromArmy(attackerUnits, _attackerArmy);
        SubscribeToUnitsDeathEventAndRemoveFromArmy(defenderUnits, _defenderArmy);
    }

    public State ProgressBattle()
    {
        if (currentBattleState != State.InProgress)
        {
            return currentBattleState;
        }

        if (attackerArmy.GetSize() <= 0 || defenderArmy.GetSize() <= 0)
        {
            currentBattleState = attackerArmy.GetSize() <= 0 ? State.DefenderWon : State.AttackerWon;
            UnsubscribeToUnitsDeathEventAndRemoveFromArmy(attackerUnits, attackerArmy);
            UnsubscribeToUnitsDeathEventAndRemoveFromArmy(defenderUnits, defenderArmy);
            
            return currentBattleState;
        }

        CombatAI.UpdateUnitCombatAI(allUnits);
        currentBattleState = State.InProgress;

        return currentBattleState;
    }


    private static void SubscribeToUnitsDeathEventAndRemoveFromArmy(List<Unit> _units, Army _army)
    {
        _units.ForEach(_unit => _unit.OnDeathEvent += _army.RemoveUnitOfType);
    }

    private static void UnsubscribeToUnitsDeathEventAndRemoveFromArmy(List<Unit> _units, Army _army)
    {
        _units.ForEach(_unit => _unit.OnDeathEvent -= _army.RemoveUnitOfType);
    }
}
