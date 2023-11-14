 using System;
using UnityEngine;
using UnityEngine.InputSystem;
 using UnityEngine.Serialization;

 public class GameUI : MonoBehaviour
{
    private enum State
    {
        General = 0,
        Isle = 1, 
        Pause = 2,
    }

    [SerializeField] private IsleZoomUI isleZoomUI = null;
    [SerializeField] private InputManager inputManager = null;

    private State currentState = State.General;
    
    private void Awake()
    {
        isleZoomUI.Hide();
    }

    private void Start()
    {
        inputManager.AvailableActions.Player.OpenAttackPanel.performed += openAttackPanel;
        inputManager.AvailableActions.Player.OpenShipPanel.performed += openShipPanel;
        inputManager.AvailableActions.Player.OpenSoldierPanel.performed += openSoldierPanel;
        inputManager.AvailableActions.Player.Select.performed += select;
        inputManager.AvailableActions.Player.CancelBack.performed += back;
    }


    private void OnDestroy()
    {
        inputManager.AvailableActions.Player.OpenAttackPanel.performed -= openAttackPanel;
        inputManager.AvailableActions.Player.OpenShipPanel.performed -= openShipPanel;
        inputManager.AvailableActions.Player.OpenSoldierPanel.performed -= openSoldierPanel;
        inputManager.AvailableActions.Player.Select.performed -= select;
        inputManager.AvailableActions.Player.CancelBack.performed -= back;
    }

    private void back(InputAction.CallbackContext _obj)
    {
        if (currentState == State.General)
        {
            //check for open windows, if exist close them else:
            //openPauseMenu
            return;
        }
       
        if(currentState == State.Isle)
        {
            if (isleZoomUI.AllWindowsClosed == false)
            {
                isleZoomUI.Back();
            }
            else
            {
                exitZoomUI();
            }
            
            return;
        }

        if (currentState == State.Pause)
        {
            //return from pause state
            return;
        }
    }

    private void select(InputAction.CallbackContext _obj)
    {
        if (currentState != State.General)
        {
            //else check if ship is hovered, is so lock on to the ship
            return;
        }
    }
    
    private void openSoldierPanel(InputAction.CallbackContext _obj)
    {
        if (currentState != State.General)
        {
            return;
        }
        
    }
    
    private void openShipPanel(InputAction.CallbackContext _obj)
    {
        if (currentState != State.General)
        {
            return;
        }
    }
    
    private void openAttackPanel(InputAction.CallbackContext _obj)
    {
        if (currentState != State.Isle)
        {
            return;
        }

        isleZoomUI.OpenAttackPanel();
    }
    
    private void exitZoomUI()
    {
        isleZoomUI.Hide();
        currentState = State.General;
    }

    private void openZoomUI()
    {
        isleZoomUI.Show();
        currentState = State.Isle;
    }
}
