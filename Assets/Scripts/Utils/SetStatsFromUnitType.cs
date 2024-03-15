using UnityEngine;

public class SetStatsFromUnitType : MonoBehaviour
{
    [SerializeField] private UnitTypeVariable unitTypeVariable = null;
    
    [SerializeField] private ResourcesVariable unitCost = null;
    [SerializeField] private IntVariable unitAttack = null;
    [SerializeField] private IntVariable unitSpeed = null;

    private UnitType lastUnitType = null;
    
    private void Update()
    {
        if (lastUnitType == unitTypeVariable.Value)
        {
            return;
        }

        lastUnitType = unitTypeVariable.Value;

        if (lastUnitType == null)
        {
            unitAttack.Value = 0;
            unitSpeed.Value = 0;
            unitCost.Value = new Resources();

            return;
        }

        UnitType _type = unitTypeVariable.Value;

        unitSpeed.Value = _type.Speed;
        unitCost.Value = _type.BaseCost;
    }
}
