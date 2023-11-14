using Missions;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MissionControl : ScriptableObject
{
    public static Mission CreateMission(MissionStage[] _stages, Isle _target, List<Ship> _shipsParticipating, List<Soldier> _soldiersParticipating)
    {
        Mission _createdMission = (new GameObject()).AddComponent<Mission>();

        _createdMission.Stages = _stages;
        _createdMission.Target = _target;
        _createdMission.Ships = _shipsParticipating;
        _createdMission.Soldiers = _soldiersParticipating;

        return _createdMission;
    }
}
