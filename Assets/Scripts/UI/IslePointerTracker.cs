using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class IslePointerTracker : MonoBehaviour
{
    public event Action<Isle> OnHoverOverIsle = null;
    public event Action OnHoverExit = null;

    public Isle CurrentlyHoveredIsle => currentlySelectedArea ? currentlySelectedArea.TrackedIsle : null;
    
    [SerializeField] private LayerMask isleBoundsMask = 0;

    private IsleHoverArea currentlySelectedArea = null;
    private Collider2D currentlySelected = null;
    private Collider2D hitThisFrame = null;

    private void Update()
    {
        hitThisFrame = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.value), isleBoundsMask);

        if (hitThisFrame == currentlySelected)
        {
            return;
        }
        
        currentlySelected = hitThisFrame;

        if (hitThisFrame)
        {
            currentlySelectedArea = hitThisFrame.GetComponent<IsleHoverArea>();
            onIsleHovered(currentlySelectedArea.TrackedIsle);
        }
        else
        {
            currentlySelectedArea = null;
            onIsleExitHover();
        }
    }

    private void onIsleHovered(Isle _isle)
    {
        OnHoverOverIsle?.Invoke(_isle);
    }

    private void onIsleExitHover()
    {
        OnHoverExit?.Invoke();
    }
}
