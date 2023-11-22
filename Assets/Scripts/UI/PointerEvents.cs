using UnityEngine;
using UnityEngine.Events;

public class PointerEvents : MonoBehaviour
{
    public UnityEvent MouseEnterEvent = null;
    public UnityEvent MouseExitEvent = null;
    public UnityEvent MouseClickEvent = null;

    private void OnMouseEnter()
    {
        MouseEnterEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnMouseExit();
    }

    private void OnMouseExit()
    {
        MouseExitEvent?.Invoke();
    }

    private void OnMouseDown()
    {
        MouseClickEvent?.Invoke();
    }
}
