using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SidePanelController : MonoBehaviour
{
    [SerializeField] private InputAction openWindowShortcut = null;
    [SerializeField] private SidePanel controlledWindow = null;
    [SerializeField] private ImageButton openButton = null;

    private void Awake()
    {
        openButton.OnButtonClicked += toggleWindow;
    }

    private void OnDestroy()
    {
        openButton.OnButtonClicked -= toggleWindow;
    }

    private void OnEnable()
    {
        openWindowShortcut.Enable();
    }

    private void OnDisable()
    {
        openWindowShortcut.Disable();
    }

    private void Update()
    {
        if (openWindowShortcut.triggered == true)
        {
            toggleWindow();
        }
    }

    private void toggleWindow()
    {
        if (controlledWindow.IsOpen())
        {
            controlledWindow.Close();
            return;
        }
        
        controlledWindow.Open();
    }
}
