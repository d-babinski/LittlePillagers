using System;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDatabase : MonoBehaviour
{
    public SoldierTemplate[] AllSoldierTemplates => soldierTemplates;
    public Dictionary<int, SoldierTemplate> AllTemplatesById => templateById;

    [SerializeField] private SoldierTemplate[] soldierTemplates = Array.Empty<SoldierTemplate>();

    private Dictionary<int, SoldierTemplate> templateById = new();

    private void Awake()
    {
        for (int i = 0; i < soldierTemplates.Length; i++)
        {
            templateById[soldierTemplates[i].SoldierId] = soldierTemplates[i];
        }
    }
}
