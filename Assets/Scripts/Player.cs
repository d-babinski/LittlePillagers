using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private ResourcesVariable currentResources = null;
    [SerializeField] private ResourcesVariable pillage = null;
    [SerializeField] private UnitTemplateDatabase availableUnitTemplates = null; 
    [SerializeField] private MissionControl missionControl = null;
    [SerializeField] private UnitDatabase unitDatabase = null;
    
    public void UpdateCycle()
    {
        currentResources.Value += Income;
    }

    public void SendUnits(UnitsSent[] _units)
    {

    }

    public void BuyUnit(UnitType _type)
    {
        unitDatabase.AddUnits(_type);
    }

    public void AddUnits(int _id, int _count)
    {
    }
    
    public Resources TotalSoldierMaintenance => calculateMaintenance(null);//TODO : fix this
    public Resources TotalShipMaintenance => calculateMaintenance(null);

    private Resources calculateMaintenance(UnitType _type)
    {
        return new Resources();
    }
    
    public Resources Income => TotalSoldierMaintenance - TotalShipMaintenance;

    //TODO: move to some kind of missionControl
    public void SendMission(Isle _isle, int _missionType, UnitsSent[] _sentShips, UnitsSent[] _sentSoldiers)
    {
        int _totalSoldiers = getSum(_sentSoldiers);
        _sentShips = removeExcessiveShips(_sentShips, _totalSoldiers);
        
        missionControl.SendMission(_isle, _missionType, _sentShips, _sentSoldiers);

        SendUnits(_sentShips);
        SendUnits(_sentSoldiers);
    }
    
    private UnitsSent[] removeExcessiveShips(UnitsSent[] _sentShips, int _requiredSeats)
    {
        //TODO:
        // also this is not too readable
        
        List<UnitsSent> _filteredShips = new();
        int _currentSeats = 0;

        for (int i = 0; i < _sentShips.Length; i++)
        {
            int _quantity = 0;
            int _shipCapacity = 0;
            
            for (int j = 0; j < _sentShips[i].Quantity; j++)
            {
                _quantity += 1;
                _currentSeats += _shipCapacity;

                if (_currentSeats >= _requiredSeats)
                {
                    _filteredShips.Add(new UnitsSent(_sentShips[i].Id, _quantity));
                    return _filteredShips.ToArray();
                }
            }
            
            _filteredShips.Add(new UnitsSent(_sentShips[i].Id, _quantity));
        }

        return _filteredShips.ToArray();
    }

    private int getSum(UnitsSent[] _sentSoldiers)
    {
        int _sum = 0;

        for (int i = 0; i < _sentSoldiers.Length; i++)
        {
            _sum += _sentSoldiers[i].Quantity;
        }

        return _sum;
    }
}
