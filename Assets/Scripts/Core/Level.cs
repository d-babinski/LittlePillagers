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
    public enum WinCondition
    {
        BeatAllStages = 0,
    }

    [Header("Level Settings")]
    [FormerlySerializedAs("VictoryCondition")][SerializeField] private WinCondition victoryCondition = WinCondition.BeatAllStages;
    [FormerlySerializedAs("IsBeaten")][SerializeField] private bool isBeaten = false;
    [SerializeField] private Transform[] spawnPoints = null;

    [Header("Events")]
    [FormerlySerializedAs("OnLevelLoaded")][SerializeField] private UnityEvent onLevelLoaded = null;
    [SerializeField] private IslandScriptableEvent zoomToIslandEvent = null;
    [SerializeField] private SimpleScriptableEvent unzoomFromIslandEvent = null;
    [SerializeField] private SimpleScriptableEvent levelBeatenEvent = null;

    [Header("State")]
    [SerializeField] private ZoomStateVariable zoomStateVariable = null;
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [FormerlySerializedAs("IslandRuntimeSet")][SerializeField] private IslandRuntimeSet islandRuntimeSet = null;
    [FormerlySerializedAs("islandPathScriptables")][SerializeField] private IslandPaths islandPathScriptable = null;

    [Header("Prefabs and References")]
    [FormerlySerializedAs("Player")][SerializeField] private Player player = null;
    [FormerlySerializedAs("IslandPrefab")][SerializeField] private Island islandPrefab = null;
    [FormerlySerializedAs("SplinePrefab")][SerializeField] private SplineComputer splinePrefab = null;
    
    public void LoadLevel(LevelSettings _level)
    {
        Island[] _spawnedIslands = spawnIslands(_level.Islands);
        islandRuntimeSet.Items = _spawnedIslands.ToList();
        islandPathScriptable.IslandSplines = spawnIslandPaths(splinePrefab, player.transform.position, _spawnedIslands);
        victoryCondition = _level.WinCondition;
        onLevelLoaded?.Invoke();
    }

    private Island[] spawnIslands(IslandType[] _islandsToSpawn)
    {
        Island[] _spawnedIslands = new Island[_islandsToSpawn.Length];

        for (int i = 0; i < _islandsToSpawn.Length; i++)
        {
            _spawnedIslands[i] = Instantiate(islandPrefab);
            _spawnedIslands[i].SetIslandType(_islandsToSpawn[i]);
            _spawnedIslands[i].transform.position = spawnPoints[i%_spawnedIslands.Length].position;
            islandRuntimeSet.Add(_spawnedIslands[i]);
        }

        return _spawnedIslands;
    }

    private Dictionary<Island, ShipPath> spawnIslandPaths(SplineComputer _splinePrefab, Vector2 _playerPos, Island[] _islands, int _splinePoints = 5)
    {
        Dictionary<Island, ShipPath> _islandSplines = new();

        foreach (var _island in _islands)
        {
            ShipPath _path = _islandSplines[_island] = SplineUtils.CreateCurvySpline(_splinePrefab, _playerPos, _island.transform.position, _splinePoints).GetComponent<ShipPath>();
            PredicateBasedActions _predicateActions = _islandSplines[_island].gameObject.AddComponent<PredicateBasedActions>();
            _predicateActions.AddPredicate(new IslandPredicate(playerStateVariable, _island));
            _predicateActions.AddPredicate(new PlayerTargetStatePredicate(playerStateVariable, PlayerTargetState.Chosen));
            _predicateActions.OnAllPredicatesFulfilledEvent.AddListener(_path.EnableVisuals);
            _predicateActions.OnAnyPredicateFailedEvent.AddListener(_path.DisableVisuals);
        }

        return _islandSplines;
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

    public void OnStageBeaten()
    {
        if (isBeaten == true)
        {
            return;
        }
            
        if (victoryCondition != WinCondition.BeatAllStages)
        {
            return;
        }

        if (isLevelCompleted() == true)
        {
            isBeaten = true;
            levelBeatenEvent.Raise();
        }
    }

    private bool isLevelCompleted()
    {
        if (victoryCondition == WinCondition.BeatAllStages)
        {
            foreach (var _island in islandRuntimeSet.Items)
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
