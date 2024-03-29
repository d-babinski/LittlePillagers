public class PlayerTargetStatePredicate : Predicate
{
    private PlayerStateVariable trackedPlayerState = null;
    private PlayerTargetState requiredState = PlayerTargetState.NotChosen;
    
    public PlayerTargetStatePredicate(PlayerStateVariable _playerState, PlayerTargetState _requiredState)
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
        return trackedPlayerState.TargetState == requiredState;
    }
}
