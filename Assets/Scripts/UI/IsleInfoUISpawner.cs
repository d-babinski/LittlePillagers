using System;
using UnityEngine;

public class IsleInfoUISpawner : MonoBehaviour
{
    [SerializeField] private IsleInfoUI[] isleUIPool = Array.Empty<IsleInfoUI>();

    private int lastShownId = 0;

    public void HideUI()
    {
        isleUIPool[lastShownId].Hide();
    }

    public void ShowUI(Vector3 _pos, string _name, Resources _res, int _population)
    {
        lastShownId++;

        if (lastShownId > isleUIPool.Length - 1)
        {
            lastShownId = 0;
        }
        
        isleUIPool[lastShownId].ShowAt(_pos);
        isleUIPool[lastShownId].SetIsleInfo(_name, _res, _population);
    }
    
    public void RefreshLastSpawned(Resources _currentResources, int _populationCount)
    {
        isleUIPool[lastShownId].RefreshIsleData(_currentResources, _populationCount);
    }
}
