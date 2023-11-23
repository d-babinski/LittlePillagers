using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float minDistance = 1.5f;
    [SerializeField] private float maxDistance = 2.5f;
    
    [SerializeField] private ClickableResource woodPrefab = null;
    [SerializeField] private ClickableResource wheatPrefab = null;
    [SerializeField] private ClickableResource metalPrefab = null;
    [SerializeField] private ClickableResource goldPrefab = null;

    public void SpawnResources(Resources _resources)
    {
        List<ClickableResource> _spawnedRes = new List<ClickableResource>();

        _spawnedRes.AddRange(spawnResources(_resources.Wood, new Resources(1,0,0,0), woodPrefab));
        _spawnedRes.AddRange(spawnResources(_resources.Wheat, new Resources(0,1,0,0), wheatPrefab));
        _spawnedRes.AddRange(spawnResources(_resources.Metal, new Resources(0,0, 1,0), metalPrefab));
        _spawnedRes.AddRange(spawnResources(_resources.Gold, new Resources(0,0,0,1), goldPrefab));

        _spawnedRes.ForEach(_clickableRes =>
        {
            _clickableRes.MoveTo(transform.position + new Vector3(Random.Range(-1f,1f), -0.5f).normalized * Random.Range(minDistance, maxDistance));
        });
    }

    private List<ClickableResource> spawnResources(int _resourcesToPut, Resources _baseVector, ClickableResource _prefabToUse)
    {
        List<ClickableResource> _spawnedList = new();
        
        while (_resourcesToPut > 0)
        {
            int _assignedRes = Mathf.Clamp(Random.Range(17, 35), 0, _resourcesToPut);
            
            ClickableResource _spawned = Instantiate(_prefabToUse);
            _spawned.transform.position = transform.position;
            _spawned.AssignResources(_baseVector * _assignedRes);
            _spawnedList.Add(_spawned);
            _resourcesToPut -= _assignedRes;
        }

        return _spawnedList;
    }
    
}

