using UnityEngine;

public class EconomyUI : MonoBehaviour
{
    [SerializeField] private EconomyRowUI pillageRow = null;
    [SerializeField] private EconomyRowUI soldierRow = null;
    [SerializeField] private EconomyRowUI shipsRow = null;
    [SerializeField] private EconomyRowUI incomeRow = null;

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
