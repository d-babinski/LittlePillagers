using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleSpawner : ScriptableObject
{
    [SerializeField] private UnitBuilder unitBuilder = null;

    public Battle AttackIsland(Army _attackerArmy, Island _island, Team _attackerTeam, Team _defenderTeam)
    {
        List<Unit> _attackerUnits = InstantiateArmy(_attackerArmy, unitBuilder);
        Army _defenderArmy = CreateCurrentStageIslandArmy(_island);
        
        List<Unit> _defenderUnits = InstantiateArmy(_defenderArmy, unitBuilder);
        IslandSpawnAreas _spawnAreas = _island.GetComponent<IslandSpawnAreas>();
        
        PlaceUnitsInArea(_attackerUnits,_spawnAreas.AttackerArea); 
        PlaceUnitsInArea(_defenderUnits, _spawnAreas.DefenderArea);
        AssignTeamToUnits(_attackerUnits, _attackerTeam);
        AssignTeamToUnits(_defenderUnits, _defenderTeam);

        return new Battle(_attackerArmy, _defenderArmy, _attackerUnits, _defenderUnits);
    }

    public static Army CreateCurrentStageIslandArmy(Island _island)
    {
        Stage _islandsStage = _island.IslandType.Stages[_island.CurrentStageNumber];
        Army _islandsArmy = CreateInstance<Army>();

        foreach (Stage.StageUnits _units in _islandsStage.UnitsInStage)
        {
            _islandsArmy.AddUnits(_units.UnitType, _units.Count);
        }

        return _islandsArmy;
    }

    public static List<Unit> InstantiateArmy(Army _army, UnitBuilder _unitSpawner)
    {
        (UnitType, int)[] _unitsToSpawn = _army.Database;
        List<Unit> _spawnedUnits = new();

        for (int i = 0; i < _unitsToSpawn.Length; i++)
        {
            for (int j = 0; j < _unitsToSpawn[i].Item2; j++)
            {
                _spawnedUnits.Add(_unitSpawner.InstantiateUnit(_unitsToSpawn[i].Item1));
            }
        }

        return _spawnedUnits;
    }

    public static void AssignTeamToUnits(List<Unit> _units, Team _team)
    {
        _units.ForEach(_unit =>
        {
            _unit.UnitTeam = _team;
            _unit.gameObject.layer = _team.UnitLayer;
        });
    }

    public static void PlaceUnitsInArea(List<Unit> _units, BoxCollider2D _area)
    {
        _units.ForEach(_unit => _unit.transform.position = _area.GetRandomPointWithinCollider());
    }
}
