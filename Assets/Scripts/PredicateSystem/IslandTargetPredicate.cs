public class IslandPredicate : Predicate
{
    private Island requiredTarget = null;
    private PlayerStateVariable trackedPlayerState = null;
    
    public IslandPredicate(PlayerStateVariable _playerState, Island _requiredTarget)
    {
        trackedPlayerState = _playerState;
        requiredTarget = _requiredTarget;
        trackedPlayerState.StateChanged += onUpdateEvent;
    }
    
    private void onUpdateEvent()
    {
        OnPredicateStateUpdated?.Invoke();
    }

    public override bool IsFulfilled()
    {
        return trackedPlayerState.CurrentTarget == requiredTarget;
    }
}
