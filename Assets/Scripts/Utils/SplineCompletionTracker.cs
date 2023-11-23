using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class SplineCompletionTracker : MonoBehaviour
{
    [SerializeField] private UnityEvent splineEndReachedActions = null;
    [SerializeField] private SplineAnimate trackedSplineAnimate = null;

    private bool isTracking = false;
    
    public void Track()
    {
        isTracking = true;
    }

    public void Update()
    {
        if (isTracking == false)
        {
            return;
        }

        if (trackedSplineAnimate.IsPlaying == false)
        {
            splineEndReachedActions?.Invoke();
            isTracking = false;
        }
    }
}
