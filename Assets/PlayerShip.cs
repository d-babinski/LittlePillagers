using Dreamteck.Splines;
using System;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public event Action OnArrival = null;

    [SerializeField] private SplineFollower splineFollower = null;

    private void OnEnable()
    {
        splineFollower.onBeginningReached += arrivalEvent;
        splineFollower.onEndReached += arrivalEvent;
    }

    private void OnDisable()
    {
        splineFollower.onBeginningReached -= arrivalEvent;
        splineFollower.onEndReached -= arrivalEvent;
    }

    private void arrivalEvent(double _progress)
    {
        OnArrival?.Invoke();
    }

    public void SwimForwardViaSpline(SplineComputer _spline)
    {
        splineFollower.spline = _spline;
        splineFollower.direction = Spline.Direction.Forward;
        splineFollower.SetPercent(0.0);
    }

    public void SwimBackwardsViaSpline(SplineComputer _spline)
    {
        splineFollower.spline = _spline;
        splineFollower.direction = Spline.Direction.Backward;
        splineFollower.SetPercent(100.0);
    }
}
 