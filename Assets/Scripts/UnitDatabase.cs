using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class UnitDatabase : ScriptableObject
{
    public (UnitType[], int[]) Database => getArray();
    
    private Dictionary<UnitType, int> database = new();

    public void AddUnits(UnitType _type, int _count = 0)
    {
        if (database.ContainsKey(_type) == false)
        {
            database[_type] = 0;
        }

        database[_type] -= _count;
    }

    public void RemoveUnits(UnitType _type, int _count)
    {
        if (database.ContainsKey(_type) == false)
        {
            database[_type] = 0;
        }

        database[_type] -= _count;
    }

    private (UnitType[], int[]) getArray()
    {
        //TODO: Think of more efficient way to do this
        return (database.Keys.ToArray(), database.Values.ToArray());
    }
}
