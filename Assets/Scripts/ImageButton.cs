using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour,IPointerClickHandler
{
    public event Action OnButtonClicked = null;

    public void OnPointerClick(PointerEventData _eventData)
    {
        OnButtonClicked?.Invoke();
    }
}
