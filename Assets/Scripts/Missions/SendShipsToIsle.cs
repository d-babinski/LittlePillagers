using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu]
    public class SendShipsToIsle : MissionStage
    {
        private bool hasIssuedMoveCommand = false;

        public override bool HasFinished(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            for (int i = 0; i < _ships.Count; i++)
            {
                if (_ships[i].IsMoving)
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
                _ships.ForEach(_ship => _ship.MoveTo(_target.ClosestDockingPoint(_ship.transform.position)));
            }
        }
    }
}