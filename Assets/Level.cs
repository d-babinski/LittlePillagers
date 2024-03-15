using CHARK.ScriptableEvents.Events;
using Dreamteck.Splines;
using ScriptableEvents.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    //TODO: Make scriptable for win conditions if checking will get complicated
    public enum WinCondition
    {
        BeatAllStages = 0,
    }

    
    public enum ZoomState
    {
        Unzoomed = 0,
        Zooomed = 1,
    }
    

    
    public WinCondition VictoryCondition = WinCondition.BeatAllStages;

    [SerializeField] private IslandScriptableEvent zoomToIslandEvent = null;
    [SerializeField] private SimpleScriptableEvent unzoomFromIslandEvent = null;
    
    [SerializeField] private Transform[] spawnPoints = null;

    private ZoomState currentZoomState = ZoomState.Unzoomed;
    
    [Header("Islands")]
    public IslandRuntimeSet IslandRuntimeSet = null;
    public Island IslandPrefab = null;
    public Player Player = null;
    public UnityEvent OnLevelLoaded = null;
    public SplineComputer SplinePrefab = null;

    private Dictionary<Island, IslandPath> islandSplines = new Dictionary<Island, IslandPath>();
    private Island currentTarget = null;


    public void OnIslandChosenAsTarget(Island _target)
    {
        currentTarget = _target;
        islandSplines[_target].EnableVisuals();
    }

    public void OnAttackTargetCanceled()
    {
        islandSplines[currentTarget].DisableVisuals();
        currentTarget = null;
    }

    public void TryZoomingToisland(Island _target)
    {
        if (currentZoomState != ZoomState.Unzoomed)
        {
            return;
        }

        currentZoomState = ZoomState.Zooomed;
        zoomToIslandEvent.Raise(_target);
    }

    public void TryUnzooming()
    {
        if (currentZoomState != ZoomState.Zooomed)
        {
            return;
        }

        currentZoomState = ZoomState.Unzoomed;
        unzoomFromIslandEvent.Raise();
    }
    
    public void LoadLevel(LevelSettings _level)
    {
        Island[] _spawnedIslands = spawnIslands(_level.Islands);
        islandSplines = spawnIslandPaths(SplinePrefab, Player.transform.position, _spawnedIslands);
        VictoryCondition = _level.WinCondition;
        OnLevelLoaded?.Invoke();
    }

    private Island[] spawnIslands(IslandType[] _islandsToSpawn)
    {
        Island[] _spawnedIslands = new Island[_islandsToSpawn.Length];

        for (int i = 0; i < _islandsToSpawn.Length; i++)
        {
            _spawnedIslands[i] = Instantiate(IslandPrefab);
            _spawnedIslands[i].SetIslandVisuals(_islandsToSpawn[i].IslandSprite);
            _spawnedIslands[i].transform.position = spawnPoints[i%_spawnedIslands.Length].position;
            _spawnedIslands[i].SetStages(_islandsToSpawn[i].Stages);
            _spawnedIslands[i].IslandName = _islandsToSpawn[i].IslandName;
            IslandRuntimeSet.Add(_spawnedIslands[i]);
        }

        return _spawnedIslands;
    }

    private Dictionary<Island, IslandPath> spawnIslandPaths(SplineComputer _splinePrefab, Vector2 _playerPos, Island[] _islands, int _splinePoints = 5)
    {
        Dictionary<Island, IslandPath> _islandSplines = new();

        foreach (var _island in _islands)
        {
            _islandSplines[_island] = SplineUtils.CreateCurvySpline(_splinePrefab, _playerPos, _island.transform.position, _splinePoints).GetComponent<IslandPath>();
            _islandSplines[_island].GetComponent<SplineRenderer>().enabled = false;
        }

        return _islandSplines;
    }

    public bool IsLevelCompleted()
    {
        if (VictoryCondition == WinCondition.BeatAllStages)
        {
            foreach (var _island in IslandRuntimeSet.Items)
            {
                if (_island.AreAllStagesBeaten() == false ) 
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }
}
