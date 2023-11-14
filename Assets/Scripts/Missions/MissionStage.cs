 using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    public abstract class MissionStage : ScriptableObject
    {
        public abstract bool HasFinished(Isle _target, List<Ship> _ships, List<Soldier> _soldiers);
        public abstract void ExecuteMission(Isle _target, List<Ship> _ships, List<Soldier> _soldiers);
    }
}