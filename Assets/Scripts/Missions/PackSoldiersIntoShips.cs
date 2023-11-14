using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu()]
    public class PackSoldiersIntoShips : MissionStage
    {
        public bool hasPacked = false;
        
        public override bool HasFinished(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            return hasPacked;
        }
        
        public override void ExecuteMission(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            if (hasPacked == false)
            {
                int _nextSeatToFill = 0;
                
                for (int i = 0; i < _ships.Count; i++)
                {
                    packSoldiersToBoat(_soldiers, _ships[i], _nextSeatToFill, _ships[i].Seats);
                    _nextSeatToFill += _ships[i].Seats;
                }

                hasPacked = true;
            }
        }
        
        private void packSoldiersToBoat(List<Soldier> _soldiers, Ship _ship, int _firstId, int _count)
        {
            for(int i = _firstId; i < _firstId+_count && i < _soldiers.Count; i++)
            {
                _ship.Pack(_soldiers[i].gameObject);
            }
        }
    }
}
