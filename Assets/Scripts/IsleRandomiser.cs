using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IsleRandomiser : MonoBehaviour
{
    [SerializeField] private int minPopulation = 10;
    [SerializeField] private int maxPopulation = 50;
    [SerializeField] private Resources minResources = new Resources(10, 10, 10, 10);
    [SerializeField] private Resources maxResources = new Resources(100, 100, 100, 100);
    [SerializeField] private int minTrees = 3;
    [SerializeField] private int maxTrees = 10;
    [SerializeField] private int minRocks = 3;
    [SerializeField] private int maxRocks = 8;
    [SerializeField] private int minWheat = 1;
    [SerializeField] private int maxWheat = 3;

    private string[] namePrefixes = new[]
    {
        "Fjall", "River","Skogur","Engi","Bardaga","Field","Eyja","Kanina","Saudfe","Steinunn","Flat","Frjosom","Falleg","Blodug","Myrkur","Rolegur","Solrikt","Graenn"
    };

    private string nameSuffix = "borg";
    
    private Isle[] isles = Array.Empty<Isle>();

    private void Start()
    {
        isles = GetComponentsInChildren<Isle>();

        foreach (Isle _isle in isles)
        {
            _isle.SetIslandName($"{namePrefixes[Random.Range(0, namePrefixes.Length)]}{nameSuffix}");
            _isle.SetInitialPopulation(Random.Range(minPopulation, maxPopulation));
            _isle.SetNaturalResources(Random.Range(minTrees, maxTrees +1),Random.Range(minRocks, maxRocks +1),Random.Range(minWheat, maxWheat +1));
            _isle.SetInitialResources(Resources.randomBetween(minResources, maxResources));
            _isle.RaiseArmy();
            _isle.BuildInitialBuildings();
        }
    }
}
