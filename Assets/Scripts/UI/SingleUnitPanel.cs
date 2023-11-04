using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class SingleUnitPanel : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public event Action<int> OnUnitBought = null;

    [SerializeField] private ImageButton buyUnitButton = null;
    [SerializeField] private UnitCountText countText = null;
    
    public bool IsSelected => isPointerHovering;

    private bool isPointerHovering = false;
    private int panelId = 0;

    private void Start()
    {
        buyUnitButton.OnButtonClicked += InvokeUnitBought;
    }

    private void OnDestroy()
    {
        buyUnitButton.OnButtonClicked -= InvokeUnitBought;
    }

    public void UpdateUnitCount(int _available, int _total)
    {
        countText.UpdateCount(_available, _total);
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        isPointerHovering = true;
    }
    
    public void OnPointerExit(PointerEventData _eventData)
    {
        isPointerHovering = false;
    }
    
    private void InvokeUnitBought()
    {
        OnUnitBought?.Invoke(panelId);    
    }
    
    public void SetPanelId(int _id)
    {
        panelId = _id;
    }
}
