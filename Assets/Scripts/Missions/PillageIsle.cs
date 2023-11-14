using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu]
    public class PillageIsle : MissionStage
    {
        private bool hasPillageFinished = false;

        public override bool HasFinished(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            return hasPillageFinished;
        }

        public override void ExecuteMission(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            if (hasPillageFinished == false)
            {
                _soldiers.ForEach(_soldier =>
                {
                    if (_soldier.IsDead)
                    {
                        return;
                    }

                    _soldier.HeldResources = _target.GetPillaged(_soldier.Capacity);
                });

                hasPillageFinished = true;
            }
        }

    }
}