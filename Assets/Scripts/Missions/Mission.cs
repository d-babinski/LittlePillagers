using System;
using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    public class Mission : MonoBehaviour
    {
        public event Action<Mission> OnMissionFinished = null;

        public List<Soldier> Soldiers = new List<Soldier>();
        public List<Ship> Ships = new List<Ship>();
        public Isle Target = null;
        public MissionStage[] Stages = Array.Empty<MissionStage>();

        private int currentStage = 0;
        private bool hasFinished = false;

        private void Start()
        {
            for (int i = 0; i < Stages.Length; i++)
            { 
                Stages[i] = Instantiate(Stages[i]);
            }
        }

        public void Update()
        {
            if (hasFinished == true)
            {
                return;
            }

            if (currentStage >= Stages.Length)
            {
                hasFinished = true;
                OnMissionFinished?.Invoke(this);
                return;
            }

            Stages[currentStage].ExecuteMission(Target, Ships, Soldiers);

            if (Stages[currentStage].HasFinished(Target, Ships, Soldiers) == false)
            {
                return;
            }

            currentStage++;
        }
    }
}