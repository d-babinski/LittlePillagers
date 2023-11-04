using DG.Tweening;
using System;
using UnityEngine;

public class IsleZoomUI : MonoBehaviour
{
    private const float DELAY_BEFORE_SHOWING_ISLE_STATS = 2f;
    public bool AllWindowsClosed => attackMenu.IsClosed;

    [SerializeField] private AttackMenuUI attackMenu = null;
    [SerializeField] private IsleInfoUI isleInfo = null;
    [SerializeField] private IsleZoomCamera zoomCam = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private Player lastAssignedPlayer = null;
    private Isle lastShownIsle = null;
    private Tween isleInfoVisibilityTween = null;

    private void Start()
    {
        attackMenu.OnMissionSent += sendMission;
    }

    private void OnDestroy()
    {
        attackMenu.OnMissionSent -= sendMission;
    }

    private void sendMission()
    {
        if (lastAssignedPlayer == false)
        {
            return;
        }

        int[] _soldierIds = lastAssignedPlayer.SoldierIds;
        int[] _shipIds = lastAssignedPlayer.ShipIds;

        int _missionType = attackMenu.GetMissionType();
        int[] _shipQuantities = attackMenu.GetShipsSent();
        int[] _soldierQuantities = attackMenu.GetSoldiersSent();

        UnitsSent[] _sentShips = new UnitsSent[_shipIds.Length];
        UnitsSent[] _sentSoldiers = new UnitsSent[_soldierIds.Length];

        for (int i = 0; i < _sentShips.Length; i++)
        {
            _sentShips[i] = new UnitsSent(_shipIds[i], _shipQuantities[i]);
        }

        for (int i = 0; i < _sentSoldiers.Length; i++)
        {
            _sentSoldiers[i] = new UnitsSent(_soldierIds[i], _soldierQuantities[i]);
        }

        lastAssignedPlayer.SendMission(lastShownIsle, _missionType, _sentShips, _sentSoldiers);
    }

    public void Show(Isle _isle)
    {
        lastShownIsle = _isle;
        zoomCam.ShowForIsle(_isle);
        attackMenu.Enable();

        isleInfoVisibilityTween = DOVirtual.DelayedCall(DELAY_BEFORE_SHOWING_ISLE_STATS, _showIsleInfo).SetUpdate(true);

        void _showIsleInfo()
        {
            isleInfo.Show();
            isleInfo.SetIsleInfo(_isle.IsleName, _isle.CurrentResources, _isle.PopulationCount);
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Hide()
    {
        isleInfoVisibilityTween?.Kill();
        canvasGroup.blocksRaycasts = false;
        isleInfo.Hide();
        zoomCam.Hide();
        attackMenu.Disable();
    }
    
    public void Back()
    {
        attackMenu.Back();
    }
    
    public void UpdateData(Player _player)
    {
        lastAssignedPlayer = _player;
        updateSoldierData(_player);
        updateShipData(_player);
    }

    private void updateSoldierData(Player _player)
    {
        string[] _unitNames = _player.GetSoldierNames;
        int[] _unitAvailability = _player.AvailableSoldiers;
        int[] _capacities = _player.SoldierCapacities;
        
        attackMenu.UpdateSoldierNames(_unitNames);
        attackMenu.UpdateSoldierCapacities(_capacities);
        attackMenu.UpdateSoldierAvailability(_unitAvailability);
    }

    private void updateShipData(Player _player)
    {
        string[] _unitNames = _player.GetShipNames;
        int[] _unitAvailability = _player.AvailableShips;
        int[] _capacities = _player.ShipCapacities;
        
        attackMenu.UpdateShipNames(_unitNames);
        attackMenu.UpdateShipCapacities(_capacities);
        attackMenu.UpdateShipAvailability(_unitAvailability);
    }
    
    public void OpenAttackPanel()
    {
        attackMenu.OpenAttackPanel();
    }
    
    public void RefreshIsleData()
    {
        if (lastShownIsle == false)
        {
            return;
        }
        
        isleInfo.RefreshIsleData(lastShownIsle.CurrentResources, lastShownIsle.PopulationCount);
    }
}
