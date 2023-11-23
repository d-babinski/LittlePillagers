using UnityEngine;
using UnityEngine.Splines;

public class SetSplineToIslandsSpline : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimator = null;

    public void SetToForwardSpline(IslandVariable _island)
    {
        splineAnimator.Container = _island.Value.SplineToReach;
    }

    public void SetToBackwardsSpline(IslandVariable _island)
    {
        splineAnimator.Container = _island.Value.ReturnSpline;
    }
}
