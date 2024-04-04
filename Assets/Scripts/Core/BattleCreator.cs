using System.Collections.Generic;
using UnityEngine;

public static class BattleCreator
{
    public static Battle CreateIslandAttackBattle(Army _attackerArmy, Island _island, Team _attackerTeam, Team _defenderTeam)
    {
        List<Unit> _attackerUnits = InstantiateArmy(_attackerArmy);
        Army _defenderArmy = CreateCurrentStageIslandArmy(_island);
        
        List<Unit> _defenderUnits = InstantiateArmy(_defenderArmy);
        IslandSpawnAreas _spawnAreas = _island.GetComponent<IslandSpawnAreas>();
        
        PlaceUnitsInArea(_attackerUnits,_spawnAreas.AttackerArea); 
        PlaceUnitsInArea(_defenderUnits, _spawnAreas.DefenderArea);
        AssignTeamToUnits(_attackerUnits, _attackerTeam);
        AssignTeamToUnits(_defenderUnits, _defenderTeam);
        
        _attackerUnits.ForEach(_unit => _unit.Face(Unit.FaceDirection.Right));
        _defenderUnits.ForEach(_unit => _unit.Face(Unit.FaceDirection.Left));

        return new Battle(_attackerArmy, _defenderArmy, _attackerUnits, _defenderUnits);
    }

    private static Army CreateCurrentStageIslandArmy(Island _island)
    {
        Stage _islandsStage = _island.IslandType.Stages[_island.CurrentStageNumber];
        Army _islandsArmy = ScriptableObject.CreateInstance<Army>();

        foreach (Stage.StageUnits _units in _islandsStage.UnitsInStage)
        {
            _islandsArmy.AddUnits(_units.UnitType, _units.Count);
        }

        return _islandsArmy;
    }

    private static List<Unit> InstantiateArmy(Army _army)
    {
        (UnitType, int)[] _unitsToSpawn = _army.Database;
        List<Unit> _spawnedUnits = new();

        for (int i = 0; i < _unitsToSpawn.Length; i++)
        {
            for (int j = 0; j < _unitsToSpawn[i].Item2; j++)
            {
                _spawnedUnits.Add(_unitsToSpawn[i].Item1.InstantiateUnit());
            }
        }

        return _spawnedUnits;
    }

    private static void AssignTeamToUnits(List<Unit> _units, Team _team)
    {
        _units.ForEach(_unit =>
        {
            _unit.UnitTeam = _team;
            _unit.gameObject.layer = _team.UnitLayer;
        });
    }

    private static void PlaceUnitsInArea(List<Unit> _units, BoxCollider2D _area)
    {
        _units.ForEach(_unit => _unit.transform.position = _area.GetRandomPointWithinCollider());
    }
}
