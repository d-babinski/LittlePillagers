using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStateVariable : ScriptableObject
{
    public Action StateChanged = null;

    public PlayerState CurrentState => currentState;
    
    private PlayerState currentState = PlayerState.Preparation;

    public void ChangeState(PlayerState _newState)
    {
        StateChanged?.Invoke();
        currentState = _newState;
    }
}
