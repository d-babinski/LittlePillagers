public class PlayerCombatStatePredicate : Predicate
{
    private PlayerStateVariable trackedPlayerState = null;
    private PlayerCombatState requiredState = PlayerCombatState.Preparation;
    
    public PlayerCombatStatePredicate(PlayerStateVariable _playerState, PlayerCombatState _requiredState)
    {
        trackedPlayerState = _playerState;
        requiredState = _requiredState;
        trackedPlayerState.StateChanged += onUpdateEvent;
    }
    
    private void onUpdateEvent()
    {
        OnPredicateStateUpdated?.Invoke();
    }

    public override bool IsFulfilled()
    {
        return trackedPlayerState.CombatState == requiredState;
    }
}
