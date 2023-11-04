using System;

public struct Mission
{
    public event Action OnMissionSuccess;
    public event Action OnMissionFailure;
    
    public int ShipId;
    public Isle Target;
    public MissionType Type;
    public int SoldiersSent;
    public int SoldierCasualties;
    public Resources AcquiredResources;
    public MissionStatus Status;

    public Mission(Isle _target, MissionType _type,int _shipId, int _soldiers)
    {
        Target = _target;
        Type = _type;
        SoldierCasualties = 0;
        SoldiersSent = _soldiers;
        ShipId = _shipId;
        AcquiredResources = new Resources();
        Status = MissionStatus.InProgress;
        OnMissionSuccess = null;
        OnMissionFailure = null;
    }

    public void ReportDeadSoldier()
    {
        SoldierCasualties += 1;
    }

    public void FailMission()
    {
        Status = MissionStatus.Failed;
        OnMissionFailure?.Invoke();
    }

    public void SucceedMission()
    {
        Status = MissionStatus.Succeeded;
        OnMissionSuccess?.Invoke();
    }
}

public enum MissionStatus
{
    InProgress = 0,
    Failed = 1,
    Succeeded = 2,
}

public enum MissionType
{
    Defend = -1,
    Pillage = 0, 
    Threaten = 1,
    Beg = 2,
    Destroy = 3,
}