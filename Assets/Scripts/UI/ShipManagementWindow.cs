using System;
using UnityEngine;

public class ShipManagementWindow : MonoBehaviour
{
    public event Action<int> OnShipBought = null;
    
    [SerializeField] private SingleUnitPanel[] shipPanels = Array.Empty<SingleUnitPanel>();
    [SerializeField] private ShipStatBlock statBlock = null;
    [SerializeField] private SidePanel windowControl = null;

    private Resources[] unitCosts = Array.Empty<Resources>();
    private Resources[] unitMaintenances = Array.Empty<Resources>();
    private int[] unitSpeeds = Array.Empty<int>();
    private int[] unitCapacities = Array.Empty<int>();
    
    private int currentlyChosenId = -1;

    private void Start()
    {
        setStatBlock(currentlyChosenId);

        int _iterator = 0;
        
        foreach (SingleUnitPanel _shipPanel in shipPanels)
        {
            _shipPanel.SetPanelId(_iterator);
            _shipPanel.OnUnitBought += onShipBought;
            _iterator++;
        }
    }

    private void OnDestroy()
    {
        foreach (SingleUnitPanel _shipPanel in shipPanels)
        {
            _shipPanel.OnUnitBought -= onShipBought;
        }
    }

    public void Open()
    {
        if (windowControl.IsOpen() == true)
        {
            return;
        }
        
        windowControl.Open();
    }

    public void Close()
    {
        if (windowControl.IsOpen() == false)
        {
            return;
        }
        
        windowControl.Close();
    }

    public void Toggle()
    {
        if (windowControl.IsOpen())
        {
            windowControl.Close();
            return;
        }
        
        windowControl.Open();
    }

    public void CloseWithoutTransition()
    {
        windowControl.CloseInstantly();
    }

    private void onShipBought(int _id)
    {
        OnShipBought?.Invoke(_id);
    }

    private void Update()
    {
        int _currentlySelected = -1;
        
        for (int i = 0; i < shipPanels.Length; i++)
        {
            if (shipPanels[i].IsSelected)
            {
                _currentlySelected = i;
                break;
            }
        }

        if (currentlyChosenId != _currentlySelected)
        {
            currentlyChosenId = _currentlySelected;
            setStatBlock(_currentlySelected);
        }
    }

    public void UpdatePanelData(int[] _available, int[] _total)
    {
        for (int i = 0; i < _available.Length; i++)
        {
            if (i < shipPanels.Length)
            {
                shipPanels[i].UpdateUnitCount(_available[i], _total[i]);
            }
        }
    }

    public void UpdateUnitCosts(Resources[] _costs)
    {
        unitCosts = _costs;
    }

    public void UpdateUnitMaintenances(Resources[] _maintenance)
    {
        unitMaintenances = _maintenance;
    }

    public void UpdateSpeeds(int[] _speeds)
    {
        unitSpeeds = _speeds;
    }

    public void UpdateCapacities(int[] _capacities)
    {
        unitCapacities = _capacities;
    }

    private Resources getUnitCost(int _id)
    {
        if (_id < 0 || _id >= unitCosts.Length)
        {
            return new Resources();
        }

        return unitCosts[_id];
    }

    private Resources getUnitMaintenance(int _id)
    {
        if (_id < 0 || _id >= unitMaintenances.Length)
        {
            return new Resources();
        }

        return unitMaintenances[_id];
    }

    private int getUnitCapacity(int _id)
    {
        if (_id < 0 || _id >= unitCapacities.Length)
        {
            return 0;
        }

        return unitCapacities[_id];
    }

    private int getUnitSpeed(int _id)
    {
        if (_id < 0 || _id >= unitSpeeds.Length)
        {
            return 0;
        }

        return unitSpeeds[_id];
    }

    private void setStatBlock(int _id)
    {
        if (_id < 0)
        {
            statBlock.SetStatBlock(new Resources(),new Resources(),0,0);
            return;
        }
        
        Resources _cost = getUnitCost(_id);
        Resources _maintenance = getUnitMaintenance(_id);
        int _maxSoldierCapacity = getUnitCapacity(_id);
        int _speed = getUnitSpeed(_id);
        
        statBlock.SetStatBlock(_cost, _maintenance, _maxSoldierCapacity, _speed);
    }
}
