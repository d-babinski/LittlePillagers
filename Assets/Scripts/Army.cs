using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Army : MonoBehaviour
{
    [FormerlySerializedAs("availableSoldiers")][SerializeField] private List<SoldierTemplate> soldierTemplates = new();

    public SoldierTemplate GetSoldierTemplate(int _id) => soldiersById[_id];
    
    private Dictionary<int, SoldierTemplate> soldiersById = new();
    private Dictionary<int, int> totalSoldierCount = new();
    private Dictionary<int, int> soldiersOnMission = new();

    public void Awake()
    {
        soldierTemplates.ForEach(_soldier =>
        {
            soldiersById[_soldier.SoldierId] = _soldier;
            totalSoldierCount[_soldier.SoldierId] = 0;
            soldiersOnMission[_soldier.SoldierId] = 0;
        });
    }

    public Resources GetCurrentMaintenance()
    {
        Resources _maintenance = new Resources();

        foreach (int _soldierId in soldiersById.Keys)
        {
            _maintenance += soldiersById[_soldierId].Maintenance*totalSoldierCount[_soldierId];
        }

        return _maintenance;
    }

    public int GetAvailableSoldiersOfId(int _id)
    {
        return totalSoldierCount[_id] - soldiersOnMission[_id];
    }

    public int GetTotalSoldiersOfId(int _id)
    {
        return totalSoldierCount[_id];
    }

    public void AddSoldiers(int _id, int _count)
    {
        totalSoldierCount[_id] += _count;
    }
    
    public void AddSoldier(int _id)
    {
        AddSoldiers(_id, 1);
    }

    public void SendSoldiersOnMission(int _id, int _count)
    {
        soldiersOnMission[_id] += _count;
    }

    public void ReturnSoldierFromMission(int _id)
    {
        soldiersOnMission[_id] -= 1;
    }
    
    public void KillSoldierOnMission(int _id)
    {
        soldiersOnMission[_id]--;
        totalSoldierCount[_id]--;
    }
    
    public Resources GetSoldierBaseCost(int _id)
    {
        return soldiersById[_id].BaseCost;
    }
    
    public Resources GetSoldierBaseMaintenance(int _id)
    {
        return soldiersById[_id].Maintenance;
    }
    
    public int GetSoldierBaseAttack(int _id)
    {
        return soldiersById[_id].Attack;
    }
    
    public int GetSoldierBaseCapacity(int _id)
    {
        return soldiersById[_id].Capacity;
    }
    
    public int GetTotalSoldierCount()
    {
        int _count = 0;
        
        foreach (int _soldierId in soldiersById.Keys)
        {
            _count += totalSoldierCount[_soldierId];
        }

        return _count;
    }

    public int GetTotalOnMissionSoldierCount()
    {
        int _count = 0;
        
        foreach (int _soldierId in soldiersById.Keys)
        {
            _count += soldiersOnMission[_soldierId];
        }

        return _count;
    }

    public int GetTotalAvailableSoldierCount()
    {
        return GetTotalSoldierCount() - GetTotalOnMissionSoldierCount();
    }
    
    public string GetSoldierName(int _id)
    {
        return soldiersById[_id].SoldierName;
    }
    
    public string[] GetAllNames()
    {
        string[] _names = new string[soldiersById.Keys.Count];
        int _iterator = 0;
        
        foreach (int _key in soldiersById.Keys)
        {
            _names[_iterator] = GetSoldierName(_key);
            _iterator++;
        }

        return _names;
    }
    
    public int[] GetAllAvailabilities()
    {
        int[] _availability = new int[soldiersById.Keys.Count];
        int _iterator = 0;
        
        foreach (int _key in soldiersById.Keys)
        {
            _availability[_iterator] = GetAvailableSoldiersOfId(_key);
            _iterator++;
        }

        return _availability;
    }

    public int[] GetAllCapacities()
    {
        int[] _capacity = new int[soldiersById.Keys.Count];
        int _iterator = 0;
        
        foreach (int _key in soldiersById.Keys)
        {
            _capacity[_iterator] = GetSoldierBaseCapacity(_key);
            _iterator++;
        }

        return _capacity;
    }
    
    public int[] GetSoldierIds()
    {
        int[] _ids = new int[soldiersById.Keys.Count];
        int _iterator = 0;
        
        foreach (int _key in soldiersById.Keys)
        {
            _ids[_iterator] = _key;
            _iterator++;
        }

        return _ids;
    }
    
    public int[] GetAllSoldiers()
    {
        int[] _soldierCounts = new int[soldiersById.Keys.Count];
        int _iterator = 0;
        
        foreach (int _key in soldiersById.Keys)
        {
            _soldierCounts[_iterator] = totalSoldierCount[_key];
            _iterator++;
        }

        return _soldierCounts;
    }
    
    public Resources[] GetCosts()
    {
        Resources[] _costs = new Resources[soldiersById.Keys.Count];
        int _iterator = 0;
        
        foreach (int _key in soldiersById.Keys)
        {
            _costs[_iterator] = GetSoldierBaseCost(_key);
            _iterator++;
        }

        return _costs;
    }
    
    public Resources[] GetMaintenances()
    {
        Resources[] _maintenances = new Resources[soldiersById.Keys.Count];
        int _iterator = 0;
        
        foreach (int _key in soldiersById.Keys)
        {
            _maintenances[_iterator] = GetSoldierBaseMaintenance(_key);
            _iterator++;
        }

        return _maintenances;
    }

    public int[] GetAttacks()
    {
        int[] _attacks = new int[soldiersById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in soldiersById.Keys)
        {
            _attacks[_iterator] = GetSoldierBaseAttack(_key);
            _iterator++;
        }

        return _attacks;
    }
}
