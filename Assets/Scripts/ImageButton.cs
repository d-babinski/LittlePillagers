using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour,IPointerClickHandler
{
    public UnityEvent OnButtonClicked = null;

    public void OnPointerClick(PointerEventData _eventData)
    {
        OnButtonClicked?.Invoke();
    }
}
