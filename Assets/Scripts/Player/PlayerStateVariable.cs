using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStateVariable : ScriptableObject
{
    public Action StateChanged = null;

    public PlayerCombatState CombatState => combatState;
    public PlayerTargetState TargetState => CurrentTarget == null ? PlayerTargetState.NotChosen : PlayerTargetState.Chosen;
    
    public Island CurrentTarget => currentTarget;

    [NonSerialized] private PlayerCombatState combatState = PlayerCombatState.Preparation;
    [NonSerialized] private Island currentTarget = null;

    public void ChangeCombatState(PlayerCombatState _newCombatState)
    {
        combatState = _newCombatState;
        StateChanged?.Invoke();
    }

    public void ChangeTarget(Island _newTarget)
    {
        currentTarget = _newTarget;
        StateChanged?.Invoke();
    }
}

