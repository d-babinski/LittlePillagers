using Dreamteck.Splines;
using UnityEngine;

public class ShipPath : MonoBehaviour
{
    public SplineComputer Spline => spline;
    
    [SerializeField] private SplineComputer spline = null;
    [SerializeField] private MeshRenderer meshRenderer = null;
    [SerializeField] private SplineRenderer splineRenderer = null;
    
    public void EnableVisuals()
    {
        splineRenderer.enabled = true;
        meshRenderer.enabled = true;
    }

    public void DisableVisuals()
    {
        splineRenderer.enabled = false;
        meshRenderer.enabled = false;
    }
}
