using System;
using UnityEngine;

public class IsleManager : MonoBehaviour
{
    public event Action OnIsleDataChanged = null;
    private Isle[] isles = Array.Empty<Isle>();

    private void Awake()
    {
        isles = GetComponentsInChildren<Isle>();

        foreach (Isle _isle in isles)
        {
            _isle.OnMissionResolved += notifyOfChange;
        }
    }
    
    private void notifyOfChange()
    {
        OnIsleDataChanged?.Invoke();
    }

    private void OnDestroy()
    {
        foreach (Isle _isle in isles)
        {
            _isle.OnMissionResolved -= notifyOfChange;
        }
    }

    public void UpdateCycle()
    {
        foreach (Isle _isle in isles)
        {
            _isle.UpdateCycle();
        }
        
        notifyOfChange();
    }
}
