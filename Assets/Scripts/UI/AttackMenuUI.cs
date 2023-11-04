using DG.Tweening;
using System;
using UnityEngine;

public class AttackMenuUI : MonoBehaviour
{
    private enum State
    {
        Closed = 0,
        Ships = 1,
        Soldiers = 2,
        MissionType = 3,
    }

    public event Action OnMissionSent = null;
    public bool IsClosed => currentState == State.Closed;

    [SerializeField] private ShipsMissionWindowUI shipsWindow = null;
    [SerializeField] private SoldierMissionWindowUI soldierWindow = null;
    [SerializeField] private MissionTypeWindowUI missionTypePanel = null;

    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private ImageButton openAttackPanelButton = null;

    private State currentState = State.Closed;
    private Tween fadeTween = null;
    
    private void Awake()
    {
        shipsWindow.OnShipsChosen += onShipsConfirmedButton;
        soldierWindow.OnSoldiersChosen += onSoldiersWindowConfirmedButton;
        missionTypePanel.OnSendButtonPressed += onSendButtonPressed;
        openAttackPanelButton.OnButtonClicked += onAttackButtonPressed;
    }

    private void onSendButtonPressed()
    {
        proceed();
    }

    private void OnDestroy()
    {
        shipsWindow.OnShipsChosen -= onShipsConfirmedButton;
        soldierWindow.OnSoldiersChosen -= onSoldiersWindowConfirmedButton;
        openAttackPanelButton.OnButtonClicked -= onAttackButtonPressed;
        missionTypePanel.OnSendButtonPressed -= onSendButtonPressed;
    }

    private void onAttackButtonPressed()
    {
        if (currentState == State.Closed)
        {
            proceed();
            return;
        }
        
        if (currentState == State.Ships)
        {
            back();
        }
    }

    private void onSoldiersWindowConfirmedButton()
    {
        if (currentState != State.Soldiers)
        {
            Debug.Log("??");
            return;
        }
        
        proceed();
    }

    private void onShipsConfirmedButton()
    {
        if (currentState != State.Ships)
        {
            return;
        }
        
        proceed();
    }

    private void sendOnMission()
    {
        OnMissionSent?.Invoke();
    }
    
    private void proceed()
    {
        switch (currentState)
        {
            case State.Closed:
                shipsWindow.ResetSelection();
                shipsWindow.OpenWindow();
                currentState = State.Ships;
                break;
            case State.Ships:
                shipsWindow.CloseWindow();
                soldierWindow.ResetSelection();
                soldierWindow.SetMaxUnits(shipsWindow.TotalCapacity);
                soldierWindow.OpenWindow();
                currentState = State.Soldiers;
                break;
            case State.Soldiers:
                soldierWindow.CloseWindow();
                missionTypePanel.OpenWindow();
                missionTypePanel.SelectOption(0);
                currentState = State.MissionType;
                break;
            case State.MissionType:
                sendOnMission();
                missionTypePanel.CloseWindow();
                currentState = State.Closed;
                break;
        }
    }

    public void Back()
    {
        back();
    }
    
    private void back()
    {
        switch (currentState)
        {
            case State.Closed:
                   break;
            case State.Ships:
                shipsWindow.CloseWindow();
                currentState = State.Closed;
                break;
            case State.Soldiers:
                soldierWindow.CloseWindow();
                shipsWindow.OpenWindow();
                currentState = State.Ships;
                break;
            case State.MissionType:
                missionTypePanel.CloseWindow();
                soldierWindow.OpenWindow();
                currentState = State.Soldiers;
                break;
        }
    }

    public void Enable()
    {
        fadeTween?.Kill();
        fadeTween = canvasGroup.DOFade(1f, 0.5f).OnComplete(_enableInteraction).SetUpdate(true);

        void _enableInteraction()
        {
            currentState = State.Closed;
        }
    }
    
    public void Disable()
    {
        currentState = State.Closed;
        missionTypePanel.SetClosed();
        soldierWindow.SetClosed();
        shipsWindow.SetClosed();
        fadeTween?.Kill();
        fadeTween = canvasGroup.DOFade(0f, 0.5f).SetUpdate(true);
    }

    public void UpdateSoldierNames(string[] _names)
    {
        soldierWindow.SetUnitNames(_names);
    }

    public void UpdateSoldierAvailability(int[] _availability)
    {
        soldierWindow.SetUnitQuantity(_availability);
    }

    public void UpdateSoldierCapacities(int[] _capacity)
    {
        soldierWindow.SetUnitCapacities(_capacity);
    }
    
    public void UpdateShipNames(string[] _unitNames)
    {
        shipsWindow.SetUnitNames(_unitNames);
    }
    
    public void UpdateShipCapacities(int[] _capacities)
    {
        shipsWindow.SetShipCapacities(_capacities);
    }
    
    public void UpdateShipAvailability(int[] _unitAvailability)
    {
        shipsWindow.SetUnitQuantity(_unitAvailability);
    }
    
    public void OpenAttackPanel()
    {
        if (currentState == State.Closed)
        {
            proceed();
        }

        if (currentState == State.Ships)
        {
            back();
        }
    }
    
    public int GetMissionType()
    {
        return (int)missionTypePanel.ChosenMissionType;
    }
    
    public int[] GetShipsSent()
    {
        return shipsWindow.GetChosenQuantities();
    }
    
    public int[] GetSoldiersSent()
    {
        return soldierWindow.GetChosenQuantities();
    }
}
