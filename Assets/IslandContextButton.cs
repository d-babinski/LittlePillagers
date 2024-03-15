 using ScriptableEvents.Events;
using System;
using UnityEngine;

[RequireComponent(typeof(PointerEvents))]
public class IslandContextButton : MonoBehaviour
{
    public event Action<int, Island> OnActionClicked = null;
    public int ActionId = -1;
    
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
        OnActionClicked?.Invoke(ActionId, trackedIsland);
    }
}
