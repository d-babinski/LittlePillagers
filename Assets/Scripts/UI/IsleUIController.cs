using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class IsleUIController : MonoBehaviour
{
    public Isle LastHoveredIsland => lastHoveredIsland;

    public bool IsCurrentlyHoveringIsland => pointerTracker.CurrentlyHoveredIsle;


    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private IsleInfoUISpawner spawner = null;
    [SerializeField] private IslePointerTracker pointerTracker = null;

    private Isle lastHoveredIsland = null;

    private void OnEnable()
    {
        pointerTracker.OnHoverOverIsle += spawnUI;
        pointerTracker.OnHoverExit += hideUI;
    }
    
    private void OnDisable()
    {
        pointerTracker.OnHoverOverIsle -= spawnUI;
        pointerTracker.OnHoverExit -= hideUI;
    }
    
    private void Update()
    {
        if (pointerTracker.CurrentlyHoveredIsle)
        {
            lastHoveredIsland = pointerTracker.CurrentlyHoveredIsle;
        }
    }

    private void hideUI()
    {
        spawner.HideUI();
    }

    private void spawnUI(Isle _isle)
    {
        Vector2 _screenPoint = mainCamera.WorldToScreenPoint(_isle.transform.position)/GetComponent<RectTransform>().lossyScale.x;
        spawner.ShowUI(_screenPoint, _isle.IsleName, _isle.CurrentResources, _isle.PopulationCount);
    }
    
    public void Hide()
    {
        spawner.HideUI();
    }
    
    public void RefreshData()
    {
        spawner.RefreshLastSpawned(lastHoveredIsland.CurrentResources, lastHoveredIsland.PopulationCount);
    }
}
