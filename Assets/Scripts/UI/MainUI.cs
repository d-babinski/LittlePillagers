using DG.Tweening;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private ManagementWindow managementWindow = null; 
    [SerializeField] private IsleUIController isleUI = null;
 
    private Tween uiFadeTween = null;
    private Player assignedPlayer = null;

    private void Awake()
    {
        managementWindow.OnShipBought += buyShip;
        managementWindow.OnSoldierBought += buySoldier;
    }
    
    private void buyShip(int _id)
    {
        int[] _shipIds = assignedPlayer.ShipIds;
        
        assignedPlayer.BuyShip(_shipIds[_id]);
    }

    private void buySoldier(int _id)
    {
        int[] _soldierIds = assignedPlayer.SoldierIds;
        
        assignedPlayer.BuySoldier(_soldierIds[_id]);
    }

    public Isle GetLastHoveredIsle()
    {
        return isleUI.LastHoveredIsland;
    }
    
    public void Show()
    {
        uiFadeTween?.Kill();
        uiFadeTween = canvasGroup.DOFade(1f, 0.5f).OnComplete(() => isleUI.enabled = true).SetUpdate(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    
    public void Hide()
    {
        uiFadeTween?.Kill();
        uiFadeTween = canvasGroup.DOFade(0f, 0.5f).SetUpdate(true);
        managementWindow.CloseAllWindowswithoutTransition();
        isleUI.Hide();
        isleUI.enabled = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void ToggleShipWindow()
    {
        managementWindow.ToggleShipWindow();
    }

    public void ToggleSoldierWindow()
    {
        managementWindow.ToggleSoldierWindow();
    }
    
    public void CloseAllWindows()
    {
        managementWindow.CloseAllWindows();
    }
    
    public bool IsIslandCurrentlyHovered()
    {
        return isleUI.IsCurrentlyHoveringIsland;
    }

    public void UpdateMonth(int _month)
    {
        managementWindow.UpdateMonth(_month);
    }
    
    public void UpdatePlayerData(Player _player)
    {
        assignedPlayer = _player;
        
        Resources _pillage = _player.Pillage;
        Resources _soldierMaintenance = _player.TotalSoldierMaintenance;
        Resources _shipMaintenance = _player.TotalShipMaintenance;
        Resources _income = _player.Income;
        
        managementWindow.UpdateEconomy(_pillage, _soldierMaintenance, _shipMaintenance, _income);

        int _availableSoldiersCount = _player.AvailableSoldiersCount;
        int _totalSoldierCount = _player.TotalSoldiersCount;
        
        managementWindow.UpdateSoldierTotalCount(_availableSoldiersCount, _totalSoldierCount);

        int _availableShipCount = _player.AvailableShipsCount;
        int _totalShipCount = _player.TotalShipCount;
        
        managementWindow.UpdateShipTotalCount(_availableShipCount, _totalShipCount);

        int[] _availableShips = _player.AvailableShips;
        int[] _totalShips = _player.TotalShips;
        Resources[] _shipCosts = _player.ShipCosts;
        Resources[] _shipMaintenances = _player.ShipMaintenances;
        int[] _shipSpeeds = _player.ShipSpeeds;
        int[] _shipCapacities = _player.ShipCapacities;
        
        managementWindow.UpdateShipsData(_availableShips, _totalShips, _shipCosts, _shipMaintenances, _shipSpeeds, _shipCapacities);

        int[] _availableSoldiers = _player.AvailableSoldiers;
        int[] _totalSoldiers = _player.TotalSoldiers;
        Resources[] _soldierCosts = _player.SoldierCosts;
        Resources[] _soldierMaintenances = _player.SoldierMaintenances;
        int[] _soldierAttacks = _player.SoldierAttacks; 
        int[] _soldierCapacities = _player.SoldierCapacities;
        
        managementWindow.UpdateSoldiersData(_availableSoldiers, _totalSoldiers,_soldierCosts, _soldierMaintenances, _soldierAttacks, _soldierCapacities);

        Resources _currentResources = _player.CurrentResources;
        
        managementWindow.UpdateCurrentResources(_currentResources);
    }
    
    public void RefreshIsleData()
    {
        if (isleUI.IsCurrentlyHoveringIsland == false)
        {
            return;
        }

        isleUI.RefreshData();
    }
}
