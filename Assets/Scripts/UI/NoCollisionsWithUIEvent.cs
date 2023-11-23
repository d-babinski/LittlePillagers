using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NoCollisionsWithUIEvent : MonoBehaviour
{
    [SerializeField] private LayerMaskVariable guiMask = null;

    [Header("On no collisions")]
    [SerializeField] private UnityEvent noUICollisionActions = null;

    public void InvokeIfNoUICollisions()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.value), guiMask.Value))
        {
            return;
        }
        
        noUICollisionActions?.Invoke();
    }
}
