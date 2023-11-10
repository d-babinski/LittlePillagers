using UnityEngine;

[CreateAssetMenu]
public class UnitTemplateDatabase : ScriptableObject
{
    [SerializeField] private UnitTemplate[] availableUnits = null;

    public UnitTemplate GetTemplate(UnitType _type)
    {
        for (int i = 0; i < availableUnits.Length; i++)
        {
            if (_type == availableUnits[i].TypeOfUnit)
            {
                return availableUnits[i];
            }
        }

        return null;
    }
}
