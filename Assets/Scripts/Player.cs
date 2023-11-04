using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnDataChanged = null;
    
    [SerializeField] private ResourceSilo playerSilo = null;
    [SerializeField] private Army playerArmy = null;
    [SerializeField] private Fleet playerFleet = null;
    [SerializeField] private Pillage playerPillage = null;
    [SerializeField] private ShipBuilder shipBuilder = null;
    [SerializeField] private SoldierBuilder soldierBuilder = null;
    [SerializeField] private PointSet spawnPoints = null;
    
    public void UpdateCycle()
    {
        AddResources(Income);
        playerPillage.UpdateCycle();
    }
    
    public int TotalSoldiersCount => playerArmy.GetTotalSoldierCount();
    public int AvailableSoldiersCount =>  playerArmy.GetTotalAvailableSoldierCount();


    public void SendSoldier(int _id)
    {
        playerArmy.SendSoldiersOnMission(_id, 1);
        OnDataChanged?.Invoke();
    }
    
    public bool BuySoldier(int _id)
    {
        Resources _soldierCost = playerArmy.GetSoldierBaseCost(_id);
        
       if (playerSilo.CanAfford(_soldierCost) == false)
        {
            return false;
        }

        playerSilo.RemoveResources(_soldierCost);
        playerArmy.AddSoldier(_id);
        OnDataChanged?.Invoke();

        return true;
    }

    public void AddSoldiers(int _id, int _count)
    {
        playerArmy.AddSoldiers(_id, _count);
        OnDataChanged?.Invoke();
    }

    public void AddShips(int _id, int _count)
    {
        playerFleet.AddShips(_id, _count);
        OnDataChanged?.Invoke();
    }
    
    public void AddResources(Resources _resources)
    {
        playerSilo.AddResources(_resources);
        OnDataChanged?.Invoke();
    }

    public Resources TotalSoldierMaintenance => playerArmy.GetCurrentMaintenance();

    public void BuyShip(int _id)
    {
        Resources _shipCost = playerFleet.GetShipBaseCost(_id);

        if (playerSilo.CanAfford(_shipCost) == false)
        {
            return;
        }

        playerSilo.RemoveResources(_shipCost);
        playerFleet.AddShip(_id);
        
        OnDataChanged?.Invoke();
    }

    public Resources CurrentResources => playerSilo.CurrentResources;
    public Resources Pillage => playerPillage.PillagedThisCycle;
    public Resources TotalShipMaintenance => playerFleet.GetCurrentMaintenance();
    public int TotalShipCount => playerFleet.GetTotalShipCount();
    public int AvailableShipsCount => playerFleet.GetAvailableShipCount();
    public string[] GetSoldierNames => playerArmy.GetAllNames();
    public int[] SoldierCapacities => playerArmy.GetAllCapacities();
    public int[] AvailableSoldiers => playerArmy.GetAllAvailabilities();
    public string[] GetShipNames => playerFleet.GetAllNames();
    public int[] AvailableShips => playerFleet.GetAllAvailabilities();
    public int[] ShipCapacities => playerFleet.GetAllCapacities();
    public int[] SoldierIds => playerArmy.GetSoldierIds();
    public int[] ShipIds => playerFleet.GetShipIds();
    public Resources Income => Pillage - TotalSoldierMaintenance - TotalShipMaintenance;
    public int[] TotalShips => playerFleet.GetAllShips();
    public Resources[] ShipCosts =>  playerFleet.GetCosts();
    public Resources[] ShipMaintenances => playerFleet.GetMaintenances();
    public int[] ShipSpeeds => playerFleet.GetSpeeds();
    public int[] TotalSoldiers => playerArmy.GetAllSoldiers();
    public Resources[] SoldierCosts =>  playerArmy.GetCosts();
    public Resources[] SoldierMaintenances => playerArmy.GetMaintenances();
    public int[] SoldierAttacks => playerArmy.GetAttacks();

    public void SendMission(Isle _isle, int _missionType, UnitsSent[] _sentShips, UnitsSent[] _sentSoldiers)
    {
        Queue<int> _shipIdQueue = convertToQueue(_sentShips);
        Queue<int> _soldierIdQueue = convertToQueue(_sentSoldiers);

        List<Ship> _ships = createShipsForSoldiers(_soldierIdQueue.Count, _shipIdQueue);
        List<Soldier> _soldiers = createSoldiers(_soldierIdQueue);

        assignSoldiersToShips(_soldiers, _ships);
        
        _ships.ForEach(_ship =>
        {
            Mission _mission = new Mission(_isle, (MissionType)_missionType, _ship.TemplateId, _ship.SoldierCount);
            _ship.transform.position = spawnPoints.GetRandom();
            _ship.AssignMission(_mission);
            _ship.OnMissionFinished += wrapUpMission;
        });
        
        _soldiers.ForEach(_soldier =>
        {
            SendSoldier(_soldier.TemplateId);
        });
        
        _ships.ForEach(_ship =>
        {
            SendShip(_ship.TemplateId);
        });
        
        OnDataChanged?.Invoke();
    }
    
    private void wrapUpMission(Ship _ship)
    {
        _ship.GetSoldiers.ForEach(_soldier =>
        {
            if (_soldier.IsDead == true)
            {
                playerArmy.KillSoldierOnMission(_soldier.TemplateId);
            }
            else
            {
                playerPillage.AddPilage(_soldier.Pillaged);
                playerArmy.ReturnSoldierFromMission(_soldier.TemplateId);
            }
        });

        if (_ship.HasSunk)
        {
            playerFleet.DestroyShipOnMission(_ship.TemplateId);
        }
        else
        {
            playerFleet.ReturnShipFromMission(_ship.TemplateId);    
        }
        
        OnDataChanged?.Invoke();
    }

    private void SendShip(int _id)
    {
        playerFleet.SendShipsOnMission(_id, 1);
    }

    private void assignSoldiersToShips(List<Soldier> _soldiers, List<Ship> _ships)
    {
        int _currentShip = 0;
        
        for (int i = 0; i < _soldiers.Count; i++)
        {
            if (_ships[_currentShip].SoldierCapacity <= _ships[_currentShip].SoldierCount)
            {
                _currentShip++;
            }
            
            _ships[_currentShip].AddSoldier(_soldiers[i]);
            
            _soldiers[i].transform.parent = _ships[_currentShip].transform;
            _soldiers[i].transform.localPosition = Vector3.zero;
            _soldiers[i].gameObject.SetActive(false);
        }
    }

    private List<Soldier> createSoldiers(Queue<int> _soldierIdQueue)
    {
        List<Soldier> _soldiers = new();

        while (_soldierIdQueue.Count > 0)
        {
            _soldiers.Add(soldierBuilder.CreateSoldier(playerArmy.GetSoldierTemplate(_soldierIdQueue.Dequeue())));
        }

        return _soldiers;
    }

    private List<Ship> createShipsForSoldiers(int _soldierCount, Queue<int> _shipIds)
    {
        List<Ship> _createdShips = new();
        int _totalShipCapacity = 0;

        while (_totalShipCapacity < _soldierCount && _shipIds.Count > 0)
        {
            int _createdShip = _shipIds.Dequeue();
            _totalShipCapacity += playerFleet.GetShipMaxSoldierCapacity(_createdShip);
            _createdShips.Add(shipBuilder.BuildShip(playerFleet.GetShipTemplate(_createdShip)));
        }

        return _createdShips;
    }

    private Queue<int> convertToQueue(UnitsSent[] _units)
    {
        Queue<int> _queue = new Queue<int>();
        
        for (int i = _units.Length-1; i >= 0; i--)
        {
            for (int j = _units[i].Quantity; j>0 ; j--)
            {
                _queue.Enqueue(_units[i].Id);
            }
        }

        return _queue;
    }
}