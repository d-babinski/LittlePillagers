using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu]
    public class ReturnToShips : MissionStage
    {
        private bool hasIssuedMoveCommand = false;

        public override bool HasFinished(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            for (int i = 0; i < _soldiers.Count; i++)
            {
                if (_soldiers[i].IsMoving)
                {
                    return false;
                }
            }

            return true;
        }

        public override void ExecuteMission(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            if (hasIssuedMoveCommand == false)
            {
                hasIssuedMoveCommand = true;
                _soldiers.ForEach(_soldier => _soldier.MoveTo(_ships[Random.Range(0, _ships.Count)].transform.position));
            }
        }
    }
}
