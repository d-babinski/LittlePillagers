using System;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    [SerializeField] private List<ShipTemplate> availableShips = new();

    public ShipTemplate GetShipTemplate(int _id) => shipsById[_id];

    private Dictionary<int, ShipTemplate> shipsById = new();
    private Dictionary<int, int> totalShipCount = new();
    private Dictionary<int, int> shipsOnMission = new();

    private void Awake()
    {
        availableShips.ForEach(_ship =>
        {
            shipsById[_ship.ShipId] = _ship;
            totalShipCount[_ship.ShipId] = 0;
            shipsOnMission[_ship.ShipId] = 0;
        });
    }

    public Resources GetCurrentMaintenance()
    {
        Resources _maintenance = new Resources();

        foreach (int _shipId in shipsById.Keys)
        {
            _maintenance += shipsById[_shipId].Maintenance*totalShipCount[_shipId];
        }

        return _maintenance;
    }

    public int GetTotalShipCount()
    {
        int _count = 0;

        foreach (int _shipId in shipsById.Keys)
        {
            _count += totalShipCount[_shipId];
        }

        return _count;
    }

    public int GetAvailableShipCount()
    {
        return GetTotalShipCount() - GetTotalShipsOnMissionCount();
    }

    public int GetAvailableShipsOfId(int _id)
    {
        return totalShipCount[_id] - shipsOnMission[_id];
    }

    public int GetTotalShipsCountOfId(int _id)
    {
        return totalShipCount[_id];
    }

    public void AddShips(int _id, int _count)
    {
        totalShipCount[_id] += _count;
    }

    public void AddShip(int _id)
    {
        AddShips(_id, 1);
    }

    public void SendShipsOnMission(int _id, int _count)
    {
        shipsOnMission[_id] += _count;
    }

    public void ReturnShipFromMission(int _id)
    {
        shipsOnMission[_id]--;
    }

    public void DestroyShipOnMission(int _id)
    {
        shipsOnMission[_id]--;
        totalShipCount[_id]--;
    }

    public Resources GetShipBaseCost(int _id)
    {
        return shipsById[_id].BaseCost;
    }

    public Resources GetShipBaseMaintenance(int _id)
    {
        return shipsById[_id].Maintenance;
    }

    public int GetShipMaxSoldierCapacity(int _id)
    {
        return shipsById[_id].MaxSoldiers;
    }

    public int GetShipSpeed(int _id)
    {
        return shipsById[_id].Speed;
    }

    public int GetTotalShipsOnMissionCount()
    {
        int _count = 0;

        foreach (int _shipId in shipsById.Keys)
        {
            _count += shipsOnMission[_shipId];
        }

        return _count;
    }

    public string GetShipName(int _id)
    {
        return shipsById[_id].ShipName;
    }

    public string[] GetAllNames()
    {
        string[] _names = new string[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _names[_iterator] = GetShipName(_key);
            _iterator++;
        }

        return _names;
    }

    public int[] GetAllAvailabilities()
    {
        int[] _availability = new int[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _availability[_iterator] = GetAvailableShipsOfId(_key);
            _iterator++;
        }

        return _availability;
    }

    public int[] GetAllCapacities()
    {
        int[] _capacity = new int[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _capacity[_iterator] = GetShipMaxSoldierCapacity(_key);
            _iterator++;
        }

        return _capacity;
    }

    public int[] GetShipIds()
    {
        int[] _ids = new int[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _ids[_iterator] = _key;
            _iterator++;
        }

        return _ids;
    }

    public int[] GetAllShips()
    {
        int[] _shipCounts = new int[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _shipCounts[_iterator] = totalShipCount[_key];
            _iterator++;
        }

        return _shipCounts;
    }

    public Resources[] GetCosts()
    {
        Resources[] _costs = new Resources[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _costs[_iterator] = GetShipBaseCost(_key);
            _iterator++;
        }

        return _costs;
    }

    public Resources[] GetMaintenances()
    {
        Resources[] _costs = new Resources[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _costs[_iterator] = GetShipBaseMaintenance(_key);
            _iterator++;
        }

        return _costs;
    }

    public int[] GetSpeeds()
    {
        int[] _speeds = new int[shipsById.Keys.Count];
        int _iterator = 0;

        foreach (int _key in shipsById.Keys)
        {
            _speeds[_iterator] = GetShipSpeed(_key);
            _iterator++;
        }

        return _speeds;
    }
}
