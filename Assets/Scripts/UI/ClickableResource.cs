using UnityEngine;
using UnityEngine.Events;

public class ClickableResource : MonoBehaviour
{
    [SerializeField] private MovingProjectile movingFunction = null;
    [SerializeField] private UnityEvent<Resources> onResourcesGathered = null;
    [SerializeField] private Resources heldResources = new Resources();

    public void MoveTo(Vector2 _targetPos)
    {
        movingFunction.SetTarget(_targetPos);
    }

    public void AssignResources(Resources _resources)
    {
        heldResources = _resources;
    }

    public void GatherResources()
    {
        onResourcesGathered?.Invoke(heldResources);
    }
}
