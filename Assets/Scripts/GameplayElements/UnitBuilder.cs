using UnityEngine;

[CreateAssetMenu]
public class UnitBuilder : ScriptableObject
{
    [SerializeField] private Unit unitPrefab = null;
    
    public Unit Build(UnitTemplate _template)
    {
        Unit _createdUnit = Instantiate(unitPrefab);
        _createdUnit.UnitTemplate = _template;
        return _createdUnit;
    }
}
