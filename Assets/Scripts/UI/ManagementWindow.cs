
using System;
using UnityEngine;

public class ManagementWindow : MonoBehaviour
{
    public event Action<int> OnSoldierBought = null;
    public event Action<int> OnShipBought = null;
    
    [SerializeField] private ShipManagementWindow shipWindow = null;
    [SerializeField] private SoldierManagementWindow soldierWindow = null;
    [SerializeField] private PlayerStatsPanel statsPanel = null;
    [SerializeField] private ImageButton shipPanelButton = null;
    [SerializeField] private ImageButton soldierPanelButton = null;
    [SerializeField] private MonthTracker monthTracker = null;
    
    private void Awake()
    {
        shipWindow.OnShipBought += onShipBought;
        soldierWindow.OnSoldierBought += onSoldierBought;
        shipPanelButton.OnButtonClicked += ToggleShipWindow;
        soldierPanelButton.OnButtonClicked += ToggleSoldierWindow;
    }

    private void OnDestroy()
    {
        shipWindow.OnShipBought -= onShipBought;
        soldierWindow.OnSoldierBought -= onSoldierBought;
        shipPanelButton.OnButtonClicked -= ToggleShipWindow;
        soldierPanelButton.OnButtonClicked -= ToggleSoldierWindow;
    }

    private void onShipBought(int _id)
    {
        OnShipBought?.Invoke(_id);
    }

    private void onSoldierBought(int _id)
    {
        OnSoldierBought?.Invoke(_id);
    }

    public void ToggleShipWindow()
    {
        shipWindow.Toggle();
    }

    public void ToggleSoldierWindow()
    {
        soldierWindow.Toggle();
    }

    public void CloseAllWindows()
    {
        shipWindow.Close();
        soldierWindow.Close();
    }

    public void CloseAllWindowswithoutTransition()
    {
        shipWindow.CloseWithoutTransition();
        soldierWindow.CloseWithoutTransition();
    }

    public void UpdateMonth(int _month)
    {
        monthTracker.SetMonth(_month);
    }

    public void UpdateCurrentResources(Resources _current)
    {
        statsPanel.UpdateCurrentResources(_current);
    }
    
    public void UpdateEconomy(Resources _pillage, Resources _soldiers, Resources _ships, Resources _income)
    {
        statsPanel.UpdateEconomy(_pillage, _soldiers, _ships , _income);
    }

    public void UpdateShipsData(int[] _available, int[] _total, Resources[] _costs, Resources[] _maintenances, int[] _speeds, int[] _capacities)
    {
        shipWindow.UpdatePanelData(_available, _total);
        shipWindow.UpdateCapacities(_capacities);
        shipWindow.UpdateUnitCosts(_costs);
        shipWindow.UpdateSpeeds(_speeds);
        shipWindow.UpdateUnitMaintenances(_maintenances);
    }

    public void UpdateShipTotalCount(int _available, int _total)
    {
        statsPanel.UpdateShips(_available, _total);
    }

    public void UpdateSoldierTotalCount(int _available, int _total)
    {
        statsPanel.UpdateSoldiers(_available, _total);
    }

    public void UpdateSoldiersData(int[] _available, int[] _total, Resources[] _costs, Resources[] _maintenances, int[] _attacks, int[] _capacities)
    {
        soldierWindow.UpdatePanelData(_available, _total);
        soldierWindow.UpdateCapacities(_capacities);
        soldierWindow.UpdateUnitCosts(_costs);
        soldierWindow.UpdateUnitMaintenances(_maintenances);
        soldierWindow.UpdateAttacks(_attacks);
    }
}
