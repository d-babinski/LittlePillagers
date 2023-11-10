using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelStartPlayerResources : MonoBehaviour
{
    [System.Serializable]
    private struct InitialUnits
    {
        public int ID;
        public int Count;
    }
    
    [SerializeField] private ResourcesVariable resourcesBeingSet = null;
    [SerializeField] private ResourcesVariable startingResources = null;
    
    [SerializeField] private List<InitialUnits> initialUnits = new();


    private void Start()
    {
        resourcesBeingSet.Value = startingResources.Value;
        
        initialUnits.ForEach(_soldiersToAdd =>
        {
            //player.AddUnits(_soldiersToAdd.ID, _soldiersToAdd.Count);
        });
    }
}
