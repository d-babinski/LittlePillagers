 using ScriptableEvents.Events;
using System;
using UnityEngine;
 using UnityEngine.Serialization;

 [RequireComponent(typeof(PointerEvents))]
public class IslandContextButton : MonoBehaviour
{
    public event Action<IslandContextAction, Island> OnActionClicked = null;
    public IslandContextAction Action = IslandContextAction.None;
    
    [SerializeField] private Island trackedIsland = null;
    
    private PointerEvents trackedButton = null;

    private void Awake()
    {
        trackedButton = GetComponent<PointerEvents>();
    }

    public void SetContext(Island _trackedIsland)
    {
        trackedIsland = _trackedIsland;
    }

    public Island GetContext()
    {
        return trackedIsland;
    }
    
    private void OnEnable()
    {
        trackedButton.MouseClickEvent.AddListener(sendEvent);
    }

    private void OnDisable()
    {
        trackedButton.MouseClickEvent.RemoveListener(sendEvent);
    }
    
    private void sendEvent()
    {
        OnActionClicked?.Invoke(Action, trackedIsland);
    }
}
