using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumOfResources : MonoBehaviour
{
    [SerializeField] private List<ResourcesVariable> resourcesToSum = new();
    [SerializeField] private ResourcesVariable output = null;
    
    private void Update()
    {
        output.Value = getSumOfResources(resourcesToSum);
    }

    private Resources getSumOfResources(List<ResourcesVariable> _resourcesToSum)
    {
        Resources _sum = new Resources();

        _resourcesToSum.ForEach(_resources =>
        {
            _sum += _resources.Value;
        });

        return _sum;
    }
}
