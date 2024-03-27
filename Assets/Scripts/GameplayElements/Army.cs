using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Army : ScriptableObject
{
    public (UnitType, int)[] Database => getArray();
    public UnitType[] Keys => UnitCountDictionary.Keys.ToArray();
    
    public Dictionary<UnitType, int> UnitCountDictionary = new();

    public void AddUnits(UnitType _type, int _count = 1)
    {
        if (UnitCountDictionary.ContainsKey(_type) == false)
        {
            UnitCountDictionary[_type] = 0;
        }

        UnitCountDictionary[_type] += _count;
    }

    public void RemoveUnitOfType(UnitType _type)
    {
        RemoveUnits(_type);
    }
    
    public void RemoveUnits(UnitType _type, int _count= 1)
    {
        if (UnitCountDictionary.ContainsKey(_type) == false)
        {
            UnitCountDictionary[_type] = 0;
        }

        UnitCountDictionary[_type] -= _count;
    }

    private (UnitType, int)[] getArray()
    {
        List<(UnitType, int)> _unitList = new();
        
        foreach (var _keyValuePair in UnitCountDictionary)
        {
            _unitList.Add((_keyValuePair.Key, _keyValuePair.Value));
        }
        
        //TODO: Think of more efficient way to do this
        return _unitList.ToArray();
    }

    public int GetSize()
    {
        int _size = 0;
        
        foreach (var _keyValuePair in UnitCountDictionary)
        {
            _size += _keyValuePair.Value;
        }

        return _size;
    }
}
