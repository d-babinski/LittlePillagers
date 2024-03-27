using CHARK.ScriptableEvents.Events;
using DefaultNamespace;
using Dreamteck.Splines;
using ScriptableEvents.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Level : MonoBehaviour
{
    //TODO: Make scriptable for win conditions if checking will get complicated
    public enum WinCondition
    {
        BeatAllStages = 0,
    }

    public WinCondition VictoryCondition = WinCondition.BeatAllStages;

    [SerializeField] private IslandScriptableEvent zoomToIslandEvent = null;
    [SerializeField] private SimpleScriptableEvent unzoomFromIslandEvent = null;

    [SerializeField] private Transform[] spawnPoints = null;
    [SerializeField] private ZoomStateVariable zoomStateVariable = null;
    [SerializeField] private UnitBuilder unitSpawner = null;

    [FormerlySerializedAs("islandPathScriptables")][SerializeField] private IslandPaths islandPathScriptable = null;

    [Header("Islands")]
    public IslandRuntimeSet IslandRuntimeSet = null;
    public Island IslandPrefab = null;
    public Player Player = null;
    public UnityEvent OnLevelLoaded = null;
    public SplineComputer SplinePrefab = null;

    private Island currentTarget = null;

    public void OnIslandChosenAsTarget(Island _target)
    {
        currentTarget = _target;
        islandPathScriptable.EnableVisualsForIsland(_target);
    }

    public void OnAttackTargetCanceled()
    {
        islandPathScriptable.DisableVisualsForIsland(currentTarget);
        currentTarget = null;
    }

    public void TryZoomingToisland(Island _target)
    {
        if (zoomStateVariable.IsZoomed == true)
        {
            return;
        }

        zoomStateVariable.ChangeStateToZoomIn(_target.transform.position);
        zoomToIslandEvent.Raise(_target);
    }

    public void TryUnzooming()
    {
        if (zoomStateVariable.IsZoomed == false)
        {
            return;
        }

        zoomStateVariable.ChangeStateToUnzoom();
        unzoomFromIslandEvent.Raise();
    }

    public void LoadLevel(LevelSettings _level)
    {
        Island[] _spawnedIslands = spawnIslands(_level.Islands);
        IslandRuntimeSet.Items = _spawnedIslands.ToList();
        islandPathScriptable.IslandSplines = spawnIslandPaths(SplinePrefab, Player.transform.position, _spawnedIslands);
        VictoryCondition = _level.WinCondition;
        OnLevelLoaded?.Invoke();
    }

    private Island[] spawnIslands(IslandType[] _islandsToSpawn)
    {
        Island[] _spawnedIslands = new Island[_islandsToSpawn.Length];

        for (int i = 0; i < _islandsToSpawn.Length; i++)
        {
            _spawnedIslands[i] = Instantiate(IslandPrefab);
            _spawnedIslands[i].SetIslandType(_islandsToSpawn[i]);
            _spawnedIslands[i].transform.position = spawnPoints[i%_spawnedIslands.Length].position;
            IslandRuntimeSet.Add(_spawnedIslands[i]);
        }

        return _spawnedIslands;
    }

    private Dictionary<Island, ShipPath> spawnIslandPaths(SplineComputer _splinePrefab, Vector2 _playerPos, Island[] _islands, int _splinePoints = 5)
    {
        Dictionary<Island, ShipPath> _islandSplines = new();

        foreach (var _island in _islands)
        {
            _islandSplines[_island] = SplineUtils.CreateCurvySpline(_splinePrefab, _playerPos, _island.transform.position, _splinePoints).GetComponent<ShipPath>();
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
                if (_island.AreAllStagesBeaten() == false)
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }
}
