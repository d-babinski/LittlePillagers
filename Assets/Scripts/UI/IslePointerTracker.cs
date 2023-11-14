using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class IslePointerTracker : MonoBehaviour
{
    [SerializeField] private BoolVariable isCurrentlyHoveringOutput = null;
    [SerializeField] private Vector3Variable islePositionOutput = null;
    [SerializeField] private StringVariable isleNameOutput = null;
    [SerializeField] private ResourcesVariable isleResourcesOutput = null;
    [SerializeField] private IntVariable islePopulationOutput = null;

    [SerializeField] private LayerMaskVariable hoverLayerMask = null;

    private Collider2D hitThisFrame = null;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isCurrentlyHoveringOutput.Value = false;
            return;
        }
        
        hitThisFrame = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.value), hoverLayerMask.Value);
        
        if (isCurrentlyHoveringOutput.Value == true && hitThisFrame == false)
        {
            isCurrentlyHoveringOutput.Value = false;
            return;
        }

        if (isCurrentlyHoveringOutput.Value == false && hitThisFrame == true)
        {
            isCurrentlyHoveringOutput.Value = true;
            extractIsleInformation(hitThisFrame);
        }
    }
    
    private void extractIsleInformation(Collider2D _hitThisFrame)
    {
        IsleHoverArea _area = _hitThisFrame.GetComponent<IsleHoverArea>();

        islePositionOutput.Value = _area.TrackedIsle.transform.position;
        isleNameOutput.Value = _area.TrackedIsle.IsleName.Value;
        islePopulationOutput.Value = _area.TrackedIsle.PopulationCount.Value;
        isleResourcesOutput.Value = _area.TrackedIsle.CurrentResources.Value;
    }

}
