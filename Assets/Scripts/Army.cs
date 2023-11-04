using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    private Dictionary<int, int> totalSoldierCount = new();
    private Dictionary<int, int> soldiersOnMission = new();

    public int GetAvailableSoldiersOfId(int _id)
    {
        if (totalSoldierCount.ContainsKey(_id) == false)
        {
            totalSoldierCount[_id] = 0;
        }
        
        if (soldiersOnMission.ContainsKey(_id) == false)
        {
            soldiersOnMission[_id] = 0;
        }
        
        return totalSoldierCount[_id] - soldiersOnMission[_id];
    }

    public int GetTotalSoldiersOfId(int _id)
    {
        if (totalSoldierCount.ContainsKey(_id) == false)
        {
            totalSoldierCount[_id] = 0;
        }
        
        return totalSoldierCount[_id];
    }

    public void AddSoldiers(int _id, int _count)
    {
        if (totalSoldierCount.ContainsKey(_id) == false)
        {
            totalSoldierCount[_id] = 0;
        }
        
        totalSoldierCount[_id] += _count;
    }
    
    public void AddSoldier(int _id)
    {
        AddSoldiers(_id, 1);
    }

    public void SendSoldiersOnMission(int _id, int _count)
    {
        if (soldiersOnMission.ContainsKey(_id) == false)
        {
            soldiersOnMission[_id] = 0;
        }
        
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

    public int GetTotalSoldierCount()
    {
        int _count = 0;

        for (int i = 0; i < totalSoldierCount.Count; i++)
        {
            _count += totalSoldierCount[i];
        }

        return _count;
    }

    public int GetTotalOnMissionSoldierCount()
    {
        int _count = 0;
        
        for (int i = 0; i < soldiersOnMission.Count; i++)
        {
            _count += soldiersOnMission[i];
        }

        return _count;
    }

    public int GetTotalAvailableSoldierCount()
    {
        return GetTotalSoldierCount() - GetTotalOnMissionSoldierCount();
    }

    public int[] GetAllAvailabilities()
    {
        int[] _availability = new int[totalSoldierCount.Count];
        int _iterator = 0;

        foreach (int _key in totalSoldierCount.Keys)
        {
            _availability[_iterator] = GetAvailableSoldiersOfId(_key);
            _iterator++;
        }

        return _availability;
    }
    
    public int[] GetAllSoldiers()
    {
        int[] _soldierCounts = new int[totalSoldierCount.Count];
        int _iterator = 0;
        
        foreach (int _key in totalSoldierCount.Keys)
        {
            _soldierCounts[_iterator] = totalSoldierCount[_key];
            _iterator++;
        }

        return _soldierCounts;
    }
}
