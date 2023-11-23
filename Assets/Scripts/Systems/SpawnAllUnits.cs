using UnityEngine;

public class SpawnAllUnits : MonoBehaviour
{
    [SerializeField] private IslandVariable chosenIsland = null;
    [SerializeField] private UnitDatabase database = null;
    [SerializeField] private UnitTemplateDatabase templates = null;

    public void SpawnAllPlayerUnits()
    {
        UnitType[] _playerUnits = database.Keys;

        for (int i = 0; i <_playerUnits.Length; i++)
        {
            for (int j = 0; j < database.UnitCountDictionary[_playerUnits[i]]; j++)
            {
                Unit _spawnedUnit = templates.InstantiateType(_playerUnits[i]);
                _spawnedUnit.transform.position = chosenIsland.Value.PlayerSpawnArea.GetRandomPointWithinCollider();
            }
        }
    }
}
