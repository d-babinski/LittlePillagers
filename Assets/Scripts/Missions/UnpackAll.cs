using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu()]
    public class UnpackAll : MissionStage
    {
        private bool hasUnpacked = false;

        public override bool HasFinished(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            return hasUnpacked;
        }
        
        public override void ExecuteMission(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            if (hasUnpacked == false)
            {
                _ships.ForEach(_ship =>
                {
                    _ship.UnpackAll();
                });

                hasUnpacked = true;
            }
        }
    }
}
