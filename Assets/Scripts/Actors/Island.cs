using System;
using UnityEngine;
using UnityEngine.Events;

public class Island : MonoBehaviour
{
    public BoxCollider2D PlayerSpawnArea = null;
    public BoxCollider2D AISpawnArea = null;


    public string IslandName = "";
    public SpriteRenderer IslandVisuals = null;
    
    [SerializeField] private Stage[] stages = Array.Empty<Stage>();
    
    [Header("Events")]
    public UnityEvent<Resources> ResourcesLooted = null;
    public UnityEvent<int> StageBeatenActions = null;
    public UnityEvent OnAllStagesBeatenActions = null;

    private int currentStage = 0;

    public void SetIslandVisuals(Sprite _sprite)
    {
        IslandVisuals.sprite = _sprite;
    }
    
    public void SetStages(Stage[] _newStages)
    {
        stages = new Stage[_newStages.Length];
        
        for (int i = 0; i < _newStages.Length; i++)
        {
            stages[i] = Instantiate(_newStages[i]);
        }
    }

    public void DropResources(Resources _resourcesToDrop)
    {
        ResourcesLooted?.Invoke(_resourcesToDrop);
    }
    
    public void BeatCurrentStage()
    {
        stages[currentStage].IsBeaten = true;
        StageBeatenActions?.Invoke(currentStage);
        ResourcesLooted?.Invoke(stages[currentStage].Rewards);
        currentStage++;

        if (currentStage >= stages.Length)
        {
            OnAllStagesBeatenActions?.Invoke();
        }
    }
    
    public Stage GetCurrentStage()
    {
        return currentStage >= stages.Length ? null : stages[currentStage];
    }
    
    public bool AreAllStagesBeaten()
    {
        return currentStage >= stages.Length;
    }
}

