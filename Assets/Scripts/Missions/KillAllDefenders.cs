﻿using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu]
    public class KillAllDefenders : MissionStage
    {
        private Battlefield battlefield = null;
        private bool hasBattlefieldStarted = false;
        private bool hasBattleFinished = false;
        
        public override bool HasFinished(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            return hasBattleFinished;
        }
        
        public override void ExecuteMission(Isle _target, List<Ship> _ships, List<Soldier> _soldiers)
        {
            if (hasBattlefieldStarted == false)
            {
                battlefield = _target.Battlefield;
                hasBattlefieldStarted = true;

                _target.GetAttacked();
            
                _soldiers.ForEach(_soldier =>
                {
                    if (_soldier.IsDead)
                    {
                        return;
                    }
                
                    battlefield.AddAttacker(_soldier);
                });

                battlefield.OnBattleConcluded += battleConcluded;
            }
        }

        private void battleConcluded()
        {
            hasBattleFinished = true;
        }
    }
}
