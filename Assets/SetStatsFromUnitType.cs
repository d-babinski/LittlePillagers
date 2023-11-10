using UnityEngine;

public class SetStatsFromUnitType : MonoBehaviour
{
    [SerializeField] private UnitTypeVariable unitTypeVariable = null;
    [SerializeField] private UnitTemplateDatabase database = null;

    [SerializeField] private ResourcesVariable unitCost = null;
    [SerializeField] private ResourcesVariable unitMaintenance = null;
    [SerializeField] private IntVariable unitCapacity = null;
    [SerializeField] private IntVariable unitAttack = null;
    [SerializeField] private IntVariable unitSpeed = null;
    [SerializeField] private IntVariable unitSeats = null;

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
            unitCapacity.Value = 0;
            unitAttack.Value = 0;
            unitSpeed.Value = 0;
            unitSeats.Value = 0;
            unitCost.Value = new Resources();
            unitMaintenance.Value = new Resources();
            return;
        }

        UnitTemplate _template = database.GetTemplate(unitTypeVariable.Value);

        unitCapacity.Value = _template.Capacity;

        unitAttack.Value = _template.Attack;
        unitSpeed.Value = _template.Speed;
        unitSeats.Value = _template.MaxSoldiers;
        unitCost.Value = _template.BaseCost;
        unitMaintenance.Value = _template.Maintenance;
    }
}
