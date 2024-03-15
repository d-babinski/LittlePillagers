using Dreamteck.Splines;
using UnityEngine;

public class IslandPath : MonoBehaviour
{
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
