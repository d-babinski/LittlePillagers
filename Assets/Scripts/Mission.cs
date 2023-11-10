using System;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    private enum MissionStage
    {
        MovingTo = 0,
        Executing = 1,
        Returning = 2,
        Finished = 3,
    }
    
    public MissionType Type = MissionType.Pillage;
    public event Action<Mission> OnMissionFinished = null;

    public List<Ship> ShipsParticipating = new();
    public List<Soldier> SoldiersParticipating = new();

    public Isle Target = null;
    private MissionStage stage = MissionStage.MovingTo;

    public void StartMission()
    {
        ShipsParticipating.ForEach(_ship => _ship.MoveTo(Target.ClosestDockingPoint(_ship.transform.position)));
        stage = MissionStage.MovingTo;
    }

    private bool haveAllShipsArrived()
    {
        for (int i = 0; i < ShipsParticipating.Count; i++)
        {
            if (ShipsParticipating[i].IsMoving)
            {
                return false;
            }
        }

        return true;
    }

    public void Update()
    {
        if (stage == MissionStage.MovingTo && haveAllShipsArrived())
        {
            executeMission();
        }
    }
    
    private void executeMission()
    {
        stage = MissionStage.Executing;
        
        
    }

}



public enum MissionType
{
    Defend = -1,
    Pillage = 0, 
    Threaten = 1,
    Beg = 2,
    Destroy = 3,
}