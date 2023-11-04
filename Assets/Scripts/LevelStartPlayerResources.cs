using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPlayerResources : MonoBehaviour
{
    [System.Serializable]
    private struct InitialUnits
    {
        public int ID;
        public int Count;
    }
    
    [SerializeField] private Player player = null;
    [SerializeField] private Resources startingResources = new Resources(200, 200, 50, 250);
    [SerializeField] private List<InitialUnits> initialSoldiers = new();
    [SerializeField] private List<InitialUnits> initialShips = new();

    private void Start()
    {
        player.AddResources(startingResources);
        
        initialSoldiers.ForEach(_soldiersToAdd =>
        {
            player.AddSoldiers(_soldiersToAdd.ID, _soldiersToAdd.Count);
        });
        
        initialShips.ForEach(_shipsToAdd =>
        {
            player.AddShips(_shipsToAdd.ID, _shipsToAdd.Count);
        });
    }
}
