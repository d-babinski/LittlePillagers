using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class Island : MonoBehaviour
{
    public SplineContainer SplineToReach = null;
    public SplineContainer ReturnSpline = null;
    public BoxCollider2D PlayerSpawnArea = null;
    public BoxCollider2D AISpawnArea = null;
    [SerializeField] private Stage[] stages = Array.Empty<Stage>();
    public UnityEvent<Stage[]> StagesInitializedActions = null;
    public UnityEvent<Resources> ResourcesLooted = null;
    public UnityEvent<int> StageBeatenActions = null;

    private int beatenStages = 0;

    private void Start()
    {
        StagesInitializedActions?.Invoke(stages);
    }

    public void SetStages(Stage[] _stages)
    {
        stages = _stages;
        StagesInitializedActions?.Invoke(stages);
    }

    public void DropResources(Resources _resourcesToDrop)
    {
        ResourcesLooted?.Invoke(_resourcesToDrop);
    }
    
    public void BeatCurrentStage()
    {
        StageBeatenActions?.Invoke(beatenStages);
        ResourcesLooted?.Invoke(stages[beatenStages].Rewards);
        beatenStages++;
    }
    
    public Stage GetCurrentStage()
    {
        return stages[beatenStages];
    }
}

