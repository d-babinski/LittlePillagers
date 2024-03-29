public class IslandBeatenPredicate : Predicate
{
    private Island islandToTrack = null;
    private bool shouldBeBeaten = false;
    
    public IslandBeatenPredicate(Island _islandToTrack, bool _shouldBeBeaten)
    {
        islandToTrack = _islandToTrack;
        shouldBeBeaten = _shouldBeBeaten;
        islandToTrack.OnIslandDestroyed += onUpdateEvent;
    }
    
    private void onUpdateEvent()
    {
        OnPredicateStateUpdated?.Invoke();
    }

    public override bool IsFulfilled()
    {
        return islandToTrack.AreAllStagesBeaten() == shouldBeBeaten;
    }
}
