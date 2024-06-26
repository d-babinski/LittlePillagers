﻿using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Pillage")]
public class Pillage : Skill
{
    [SerializeField] private float effectiveness = 0.33f;
    [SerializeField] private IslandRuntimeSet availableIslands = null;
    [SerializeField] private ClickableResourceSpawner clickableResourceSpawner = null;

    public override void UseSkill(Vector2 _target)
    {
        availableIslands.Items.ForEach(_island =>
        {
          
            Stage _currentStage = _island.GetCurrentStage();

            if (_currentStage == false)
            {
                return;
            }

            clickableResourceSpawner.SpawnResources(_currentStage.Rewards*effectiveness, _island.transform.position);
        });
    }
}
