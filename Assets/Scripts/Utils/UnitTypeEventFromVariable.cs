using ScriptableEvents.Events;
using UnityEngine;

public class UnitTypeEventFromVariable : MonoBehaviour
{
    [SerializeField] private UnitTypeVariable variableToGrabTypeFrom = null;
    [SerializeField] private UnitTypeScriptableEvent eventToRaise = null;

    public void RaiseEvent()
    {
        if (eventToRaise == null)
        {
            Debug.LogError("Trying to call a null event");
            return;
        }
        
        eventToRaise.Raise(variableToGrabTypeFrom == null ? null : variableToGrabTypeFrom.Value);
    }
}
