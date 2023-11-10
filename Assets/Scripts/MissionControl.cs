using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionControl : MonoBehaviour
{
    [SerializeField] private ShipBuilder shipBuilder = null;
    [SerializeField] private SoldierBuilder soldierBuilder = null;
    [SerializeField] private PointSet spawnPoints = null;
    [SerializeField] private UnitDatabase unitDatabase = null;
    
    public void SendMission(Isle _isle, int _missionType, UnitsSent[] _sentShips, UnitsSent[] _sentSoldiers)
    {
        Queue<int> _shipIdQueue = convertToQueue(_sentShips);
        Queue<int> _soldierIdQueue = convertToQueue(_sentSoldiers);

        List<Ship> _ships = createShips(_shipIdQueue);
        List<Soldier> _soldiers = createSoldiers(_soldierIdQueue);

        assignSoldiersToShips(_soldiers, _ships);
        
        _ships.ForEach(_ship =>
        {
            Mission _mission = new Mission(_isle, (MissionType)_missionType, _ship.TemplateId, _ship.SoldierCount);
            _ship.transform.position = spawnPoints.GetRandom();
            _ship.AssignMission(_mission);
            _ship.OnMissionFinished += wrapUpMission;
        });
    }
    
    private void wrapUpMission(Ship _ship)
    {
        
    }
    
    private void assignSoldiersToShips(List<Soldier> _soldiers, List<Ship> _ships)
    {
        int _currentShip = 0;
        
        for (int i = 0; i < _soldiers.Count; i++)
        {
            if (_ships[_currentShip].Seats <= _ships[_currentShip].SoldierCount)
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
            //_soldiers.Add(soldierBuilder.CreateSoldier(unitDatabase.GetUnitOfId(_soldierIdQueue.Dequeue())));
        }

        return _soldiers;
    }

    private List<Ship> createShips(Queue<int> _shipIds)
    {
        List<Ship> _createdShips = new();

        while (_shipIds.Count > 0)
        {
            int _createdShip = _shipIds.Dequeue();
           // _createdShips.Add(shipBuilder.BuildShip(unitDatabase.GetUnitOfId(_createdShip)));
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
