using UnityEngine;

public class SpawnCurrentStage : MonoBehaviour
{
    [SerializeField] private Island islandToSpawnTo = null;
    [SerializeField] private UnitTemplateDatabase unitTemplates = null;
    
    public void SpawnStage()
    {
        Stage _stage = islandToSpawnTo.GetCurrentStage();

        for (int i = 0; i < _stage.UnitsInStage.Length; i++)
        {
            for (int j = 0; j < _stage.UnitsInStage[i].Count; j++)
            {
                Unit _spawnedUnit = unitTemplates.InstantiateType(_stage.UnitsInStage[i].UnitType);
                _spawnedUnit.transform.position = islandToSpawnTo.AISpawnArea.GetRandomPointWithinCollider();
            }
        }
    }
}
