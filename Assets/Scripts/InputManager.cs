using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputActions AvailableActions => availableActions;
    
    private InputActions availableActions = null;

    private void Awake()
    {
        availableActions = new InputActions();
    }

    void OnEnable()
    {
        availableActions.Enable();
    }
    void OnDisable()
    {
        availableActions.Enable();
    }
}
