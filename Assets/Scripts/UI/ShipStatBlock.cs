using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipStatBlock : MonoBehaviour
{
    [SerializeField] private ResourceBlockUI costResourceBlock = null;
    [SerializeField] private ResourceBlockUI maintenanceResourceBlock = null;
    [SerializeField] private TextMeshProUGUI maxSoldierCapValueTextComponent = null;
    [SerializeField] private TextMeshProUGUI speedValueTextComponent = null;

    public void SetStatBlock(Resources _cost, Resources _maintenance, int _maxSoldierCapacity, int _speed)
    {
        costResourceBlock.SetResources(_cost);
        maintenanceResourceBlock.SetResources(_maintenance);
        maxSoldierCapValueTextComponent.text = _maxSoldierCapacity > 0 ? _maxSoldierCapacity.ToString() : "-";
        speedValueTextComponent.text = _speed > 0 ? _speed.ToString() : "-";
    }
}
