using UnityEngine;

public class ResourceSilo : MonoBehaviour
{
    public Resources CurrentResources => currentResources;
    
    private Resources currentResources = new Resources();

    public Resources AddResources(Resources _addedResources)
    {
        currentResources += _addedResources;
        return currentResources;
    }
    
    public Resources RemoveResources(Resources _removedResources)
    {
        currentResources -= _removedResources;
        return currentResources;
    }
    
    public bool CanAfford(Resources _cost)
    {
        return currentResources/_cost >= 1f;
    }
}
