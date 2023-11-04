using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleStatsUI : MonoBehaviour
{
    [SerializeField] private IsleResourceUI resources = null;
    [SerializeField] private PopulationCountUI population = null;
    
    public void SetResources(Resources _currentResources, Resources _maxResources)
    {
        resources.SetValues(_currentResources, _maxResources);
    }
    public void SetPopulation(int _population)
    {
        population.SetNewPopulation(_population);
    }
}
