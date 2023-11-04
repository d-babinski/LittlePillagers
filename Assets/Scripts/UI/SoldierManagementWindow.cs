using System;
using UnityEngine;

public class SoldierManagementWindow : MonoBehaviour
{
    public event Action<int> OnSoldierBought = null;
    
    [SerializeField] private SingleUnitPanel[] soldierPanels = Array.Empty<SingleUnitPanel>();
    [SerializeField] private SoldierStatBlock statBlock = null;
    [SerializeField] private SidePanel windowControl = null;
    
    private Resources[] unitCosts = Array.Empty<Resources>();
    private Resources[] unitMaintenances = Array.Empty<Resources>();
    private int[] unitAttacks = Array.Empty<int>();
    private int[] unitCapacities = Array.Empty<int>();

    private int currentlyChosenId = -1;

    private void Start()
    {
        setStatBlock(currentlyChosenId);

        int _iterator = 0;
        
        foreach (SingleUnitPanel _soldierPanel in soldierPanels)
        {
            _soldierPanel.SetPanelId(_iterator);
            _soldierPanel.OnUnitBought += TryBuyingSoldier;
            _iterator++;
        }
    }

    private void OnDestroy()
    {
        foreach (SingleUnitPanel _soldierPanel in soldierPanels)
        {
            _soldierPanel.OnUnitBought -= TryBuyingSoldier;
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

    private void TryBuyingSoldier(int _id)
    {
        OnSoldierBought?.Invoke(_id);
    }

    private void Update()
    {
        int _currentlySelected = -1;
        
        for (int i = 0; i < soldierPanels.Length; i++)
        {
            if (soldierPanels[i].IsSelected)
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

    private void setStatBlock(int _id)
    {
        if (_id < 0)
        {
            statBlock.SetStatBlock(new Resources(),new Resources(),0,0);
            return;
        }
        
        Resources _cost = getUnitCost(_id);
        Resources _maintenance = getUnitMaintenance(_id);
        int _attack = getUnitAttack(_id);
        int _capacity = getUnitCapacity(_id);
        
        statBlock.SetStatBlock(_cost, _maintenance, _attack, _capacity);
    }
    
    public void UpdatePanelData(int[] _available, int[] _total)
    {
        for (int i = 0; i < _available.Length; i++)
        {
            if (i < soldierPanels.Length)
            {
                soldierPanels[i].UpdateUnitCount(_available[i], _total[i]);
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

    public void UpdateAttacks(int[] _attacks)
    {
        unitAttacks = _attacks;
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

    private int getUnitAttack(int _id)
    {
        if (_id < 0 || _id >= unitAttacks.Length)
        {
            return 0;
        }

        return unitAttacks[_id];
    }
}