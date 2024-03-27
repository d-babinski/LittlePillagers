using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClickableResourceSpawner : ScriptableObject
{
    [SerializeField] private float minDistance = 1.85f;
    [SerializeField] private float maxDistance = 2.25f;
    [SerializeField] private Vector2 spawnPositionOffset = new Vector2(0.85f, 0.7f);
    
    [SerializeField] private ClickableResource woodPrefab = null;
    [SerializeField] private ClickableResource wheatPrefab = null;
    [SerializeField] private ClickableResource metalPrefab = null;
    [SerializeField] private ClickableResource goldPrefab = null;

    public void SpawnResources(Resources _resources, Vector2 _position)
    {
        List<ClickableResource> _spawnedRes = new List<ClickableResource>();
        Vector2 _spawnPos = _position + spawnPositionOffset;

        _spawnedRes.AddRange(spawnResources(_spawnPos, _resources.Wood, Resources.OneWood, woodPrefab));
        _spawnedRes.AddRange(spawnResources(_spawnPos, _resources.Wheat, Resources.OneWheat, wheatPrefab));
        _spawnedRes.AddRange(spawnResources(_spawnPos, _resources.Metal, Resources.OneIron, metalPrefab));
        _spawnedRes.AddRange(spawnResources(_spawnPos, _resources.Gold, Resources.OneGold, goldPrefab));

        _spawnedRes.ForEach(_clickableRes =>
        {
            _clickableRes.MoveTo(_spawnPos + new Vector2(Random.Range(-1f,1f), -0.5f).normalized * Random.Range(minDistance, maxDistance));
        });
    }

    private List<ClickableResource> spawnResources(Vector2 _spawnPos, int _spawnedResourcesAmount, Resources _baseVector, ClickableResource _prefabToUse)
    {
        List<ClickableResource> _spawnedList = new();
        
        while (_spawnedResourcesAmount > 0)
        {
            int _assignedRes = Mathf.Clamp(Random.Range(17, 35), 0, _spawnedResourcesAmount);
            
            ClickableResource _spawned = Instantiate(_prefabToUse);
            _spawned.transform.position = _spawnPos;
            _spawned.AssignResources(_baseVector * _assignedRes);
            _spawnedList.Add(_spawned);
            _spawnedResourcesAmount -= _assignedRes;
        }

        return _spawnedList;
    }
    
}

