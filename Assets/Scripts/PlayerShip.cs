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

    public bool IsAtBeggining()
    {
        return splineFollower.GetPercent() <= 1f;
    }

    public bool IsAtEnd()
    {
        return splineFollower.GetPercent() >= 99f;
    }

    public void MoveForwardAPath(ShipPath _path)
    {
        splineFollower.spline = _path.Spline;
        splineFollower.direction = Spline.Direction.Forward;
        splineFollower.SetPercent(0.0);
    }

    public void MoveBackwardsAPath(ShipPath _path)
    {
        splineFollower.spline = _path.Spline;
        splineFollower.direction = Spline.Direction.Backward;
        splineFollower.SetPercent(100.0);
    }
}
 