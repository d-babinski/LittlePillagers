using UnityEngine;

public class PlayerStatsPanel : MonoBehaviour
{
    [SerializeField] private CurrentResourceUI currentResourceUI = null;
    [SerializeField] private MilitaryUI militaryUI = null;
    [SerializeField] private EconomyUI economyUI = null;

    public void UpdateCurrentResources(Resources _currentResources)
    {
        currentResourceUI.SetCurrentResources(_currentResources);
    }

    public void UpdateShips( int _availableShips, int _totalShips)
    {
        militaryUI.SetShipAvailability(_availableShips, _totalShips);
    }

    public void UpdateSoldiers(int _availableSoldiers, int _totalSoldiers)
    {
        militaryUI.SetSoldierAvailability(_availableSoldiers, _totalSoldiers);
    }
    
    public void UpdateEconomy(Resources _pillage, Resources _soldierMaintenance, Resources _shipMaintenance, Resources _income)
    {
        economyUI.SetPillage(_pillage);
        economyUI.SetSoldierMaintenance(_soldierMaintenance);
        economyUI.SetShipMaintenance(_shipMaintenance);
        economyUI.SetIncome(_income);
    }
}
