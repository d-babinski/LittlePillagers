using System;
using UnityEngine;

[CreateAssetMenu]
public class ZoomStateVariable : ScriptableObject
{
    public event Action ZoomStateChangedEvent = null;
    
    public bool IsZoomed => isZoomedIn;
    public Vector2 ZoomTarget => lastZoomTarget;
    
    [NonSerialized] private Vector2 lastZoomTarget = Vector2.zero; 
    [NonSerialized] private bool isZoomedIn = false;

    public void ChangeStateToZoomIn(Vector2 _target)
    {
        isZoomedIn = true;
        lastZoomTarget = _target;
        ZoomStateChangedEvent?.Invoke();
    }

    public void ChangeStateToUnzoom()
    {
        isZoomedIn = false;
        ZoomStateChangedEvent?.Invoke();
    }
}
