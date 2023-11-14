using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class UnitDatabase : ScriptableObject
{
    public (UnitType[], int[]) Database => getArray();
    
    public Dictionary<UnitType, int> UnitCountDictionary = new();

    public void AddUnits(UnitType _type, int _count = 1)
    {
        if (UnitCountDictionary.ContainsKey(_type) == false)
        {
            UnitCountDictionary[_type] = 0;
        }

        UnitCountDictionary[_type] += _count;
    }

    public void RemoveUnits(UnitType _type, int _count= 1)
    {
        if (UnitCountDictionary.ContainsKey(_type) == false)
        {
            UnitCountDictionary[_type] = 0;
        }

        UnitCountDictionary[_type] -= _count;
    }

    private (UnitType[], int[]) getArray()
    {
        //TODO: Think of more efficient way to do this
        return (UnitCountDictionary.Keys.ToArray(), UnitCountDictionary.Values.ToArray());
    }
}
