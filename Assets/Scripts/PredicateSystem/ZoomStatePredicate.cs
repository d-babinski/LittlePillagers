public class ZoomStatePredicate : Predicate
{
    private ZoomStateVariable trackedZoomState = null;
    private bool requiresZoom = false;
    
    public ZoomStatePredicate(ZoomStateVariable _zoomState, bool _requiresZoom)
    {
        trackedZoomState = _zoomState;
        requiresZoom = _requiresZoom;
        trackedZoomState.ZoomStateChangedEvent += onUpdateEvent;
    }
    
    private void onUpdateEvent()
    {
        OnPredicateStateUpdated?.Invoke();
    }

    public override bool IsFulfilled()
    {
        return trackedZoomState.IsZoomed == requiresZoom;
    }
}
