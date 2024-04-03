using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Island : MonoBehaviour
{
    public Team IslandTeam = null;
    public event Action<int> OnStageChange = null;
    public event Action OnIslandDestroyed = null;
    public int CurrentStageNumber => currentStageNumber;
    public int TotalStageCount => IslandType.Stages.Length;
    public Stage GetStageSetup(int _number) => IslandType.Stages[_number];
    public Stage GetCurrentStage() => CurrentStageNumber >= IslandType.Stages.Length ? null : IslandType.Stages[CurrentStageNumber];
    public IslandType IslandType => typeOfIsland;

    [FormerlySerializedAs("IslandSprite")][FormerlySerializedAs("IslandVisuals")][SerializeField] private SpriteRenderer islandSpriteRenderer = null;

    private int currentStageNumber = 0;
    private IslandType typeOfIsland = null;

    public void SetIslandType(IslandType _newIslandType)
    {
        typeOfIsland = _newIslandType;
        islandSpriteRenderer.sprite = _newIslandType.IslandSprite;
        OnStageChange?.Invoke(currentStageNumber);
    }

    public void SetStage(int _newStage)
    {
        currentStageNumber = _newStage;
        OnStageChange?.Invoke(currentStageNumber);
    }
    
    public void BeatStage()
    {
        currentStageNumber++;
        OnStageChange?.Invoke(currentStageNumber);

        if (AreAllStagesBeaten() == true)
        {
            OnIslandDestroyed?.Invoke();
        }
    }

    public bool AreAllStagesBeaten()
    {
        return currentStageNumber >= typeOfIsland.Stages.Length;
    }
}

