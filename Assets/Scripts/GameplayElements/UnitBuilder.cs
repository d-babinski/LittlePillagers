using UnityEngine;

[CreateAssetMenu]
public class UnitBuilder : ScriptableObject
{
    [SerializeField] private Unit unitPrefab = null;
    
    public Unit InstantiateUnit(UnitType _type)
    {
        Unit _createdUnit = Instantiate(unitPrefab);
        _createdUnit.UnitType = _type;
        return _createdUnit;
    }
}
