using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnButtonClicked = null;
    public UnityEvent OnHover = null;
    public UnityEvent OnHoverExit = null;

    public void OnDisable()
    {
        OnHoverExit?.Invoke();
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        OnButtonClicked?.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover?.Invoke();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit?.Invoke();
    }
}
