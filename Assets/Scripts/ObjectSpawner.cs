using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] resourcePrefabVariants = Array.Empty<GameObject>();
    [SerializeField] private Collider2D spawnArea = null; 
    private List<GameObject> spawnedObjects = new();

    public void SpawnNewObject()
    {
        addObjects(1);
    }
    
    public void SetNumberOfSpawnedObjects(int _number)
    {
        if (_number == spawnedObjects.Count)
        {
            return;
        }
        
        if (_number > spawnedObjects.Count)
        {
            addObjects(_number - spawnedObjects.Count);
            return;
        }

        removeObjects(spawnedObjects.Count - _number);
    }
    
    private void removeObjects(int _removed)
    {
        int _count = spawnedObjects.Count;
        
        for (int i = 0; i < _removed; i++)
        {
            int _removedId = _count - 1 - i;
            
            Destroy(spawnedObjects[_removedId]);
            spawnedObjects.RemoveAt(_removedId);
        }
    }

    private void addObjects(int _added)
    {
        //TODO: simplify
        for (int i = 0; i < _added; i++)
        {
            Vector2 _spawnPos = (spawnArea.GetRandomPointWithinCollider());
            spawnedObjects.Add(Instantiate(resourcePrefabVariants[Random.Range(0, resourcePrefabVariants.Length)], _spawnPos, Quaternion.identity, transform));
        }
    }

    
    
}
