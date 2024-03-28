using System.Collections.Generic;
using UnityEngine;

public class LevelStartPlayerResources : MonoBehaviour
{
    [System.Serializable]
    private struct InitialUnits
    {
        public UnitType Type;
        public int Count;
    }
    
    [SerializeField] private ResourcesVariable resourcesBeingSet = null;
    [SerializeField] private ResourcesVariable startingResources = null;
    [SerializeField] private Army playerDatabase = null;
    
    [SerializeField] private List<InitialUnits> initialUnits = new();


    private void Start()
    {
        resourcesBeingSet.Value = startingResources.Value;
        playerDatabase.Clear();
        
        initialUnits.ForEach(_soldiersToAdd =>
        {
            playerDatabase.SetUnits(_soldiersToAdd.Type, _soldiersToAdd.Count); 
        });
    }
}
