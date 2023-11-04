 using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    private enum State
    {
        General = 0,
        Isle = 1, 
        Pause = 2,
    }

    [SerializeField] private MainUI mainGameUI = null;
    [SerializeField] private IsleZoomUI isleZoomUI = null;
    [SerializeField] private InputManager inputManager = null;
    [SerializeField] private Player player = null;
    [SerializeField] private IsleManager isleManager = null;
    [SerializeField] private TimeCycler timeCycler = null;

    private State currentState = State.General;
    
    private void Awake()
    {
        mainGameUI.Show();
        isleZoomUI.Hide();
    }

    private void Start()
    {
        inputManager.AvailableActions.Player.OpenAttackPanel.performed += openAttackPanel;
        inputManager.AvailableActions.Player.OpenShipPanel.performed += openShipPanel;
        inputManager.AvailableActions.Player.OpenSoldierPanel.performed += openSoldierPanel;
        inputManager.AvailableActions.Player.Select.performed += select;
        inputManager.AvailableActions.Player.CancelBack.performed += back;

        updateCycleData();
        updatePlayerData();
        updateIsleData();

        isleManager.OnIsleDataChanged += updateIsleData;
        timeCycler.OnNewCycle += updateCycleData;
        player.OnDataChanged += updatePlayerData;
    }

    private void updateIsleData()
    {
        isleZoomUI.RefreshIsleData();
        mainGameUI.RefreshIsleData();
    }

    private void updateCycleData()
    {
        mainGameUI.UpdateMonth(timeCycler.CurrentCycle);
    }

    private void updatePlayerData()
    {
        mainGameUI.UpdatePlayerData(player);
        isleZoomUI.UpdateData(player);
    }

    private void OnDestroy()
    {
        inputManager.AvailableActions.Player.OpenAttackPanel.performed -= openAttackPanel;
        inputManager.AvailableActions.Player.OpenShipPanel.performed -= openShipPanel;
        inputManager.AvailableActions.Player.OpenSoldierPanel.performed -= openSoldierPanel;
        inputManager.AvailableActions.Player.Select.performed -= select;
        inputManager.AvailableActions.Player.CancelBack.performed -= back;
        
        player.OnDataChanged -= updatePlayerData;
        isleManager.OnIsleDataChanged += updateIsleData;
        timeCycler.OnNewCycle += updateCycleData;
    }

    private void back(InputAction.CallbackContext _obj)
    {
        if (currentState == State.General)
        {
            mainGameUI.CloseAllWindows();
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

        if (mainGameUI.IsIslandCurrentlyHovered())
        {
            openZoomUI(mainGameUI.GetLastHoveredIsle());
        }
    }
    
    private void openSoldierPanel(InputAction.CallbackContext _obj)
    {
        if (currentState != State.General)
        {
            return;
        }
        
        mainGameUI.ToggleSoldierWindow();
    }
    
    private void openShipPanel(InputAction.CallbackContext _obj)
    {
        if (currentState != State.General)
        {
            return;
        }
        
        mainGameUI.ToggleShipWindow();
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
        mainGameUI.Show();
        currentState = State.General;
    }

    private void openZoomUI(Isle _isle)
    {
        mainGameUI.Hide();
        isleZoomUI.Show(_isle);
        currentState = State.Isle;
    }
}
