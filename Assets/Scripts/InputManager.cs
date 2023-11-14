using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public InputActions AvailableActions => availableActions;
    
    private InputActions availableActions = null;

    public UnityEvent OnPauseClicked = null;
    public UnityEvent OnSelectClicked = null;
    public UnityEvent OnSoldierPanelOpen = null;
    public UnityEvent OnShipPanelOpen = null;
    public UnityEvent OnAttackPanelOpen = null;

    private void Awake()
    {
        availableActions = new InputActions();
    }
    
    private void Update()
    {
        if (availableActions.Player.Pause.triggered)
        {
            OnPauseClicked?.Invoke();
        }

        if (availableActions.Player.Select.triggered)
        {
            OnSelectClicked?.Invoke();
        }

        if (availableActions.Player.OpenSoldierPanel.triggered)
        {
            OnSoldierPanelOpen?.Invoke();
        }

        if (availableActions.Player.OpenShipPanel.triggered)
        {
            OnShipPanelOpen?.Invoke();
        }

        if (availableActions.Player.OpenSoldierPanel.triggered)
        {
            OnAttackPanelOpen?.Invoke();
        }
    }


    void OnEnable()
    {
        availableActions.Enable();
    }
    void OnDisable()
    {
        availableActions.Disable();
    }
}
