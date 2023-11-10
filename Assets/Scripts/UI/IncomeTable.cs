using UnityEngine;

public class EconomyUI : MonoBehaviour
{
    [SerializeField] private ColoredResourceBlock pillageRow = null;
    [SerializeField] private ColoredResourceBlock soldierRow = null;
    [SerializeField] private ColoredResourceBlock shipsRow = null;
    [SerializeField] private ColoredResourceBlock incomeRow = null;

    public void SetIncome(Resources _income)
    {
        incomeRow.SetValues(_income);
    }

    public void SetPillage(Resources _pillage)
    {
        pillageRow.SetValues(_pillage);
    }
    
    public void SetSoldierMaintenance(Resources _maintenance)
    {
        soldierRow.SetValues(_maintenance*-1f);
    }

    public void SetShipMaintenance(Resources _maintenance)
    {
        shipsRow.SetValues(_maintenance*-1f);
    }
}
