using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipStatBlock : MonoBehaviour,IStatBlock
{
    [SerializeField] private ResourceBlockUI costResourceBlock = null;
    [SerializeField] private ResourceBlockUI maintenanceResourceBlock = null;
    [SerializeField] private TextMeshProUGUI maxSoldierCapValueTextComponent = null;
    [SerializeField] private TextMeshProUGUI speedValueTextComponent = null;

    public void SetStatBlock(UnitTemplate _unitTemplate)
    {
        //costResourceBlock.SetResources(_unitTemplate.BaseCost);
        //maintenanceResourceBlock.SetResources(_unitTemplate.Maintenance);
        maxSoldierCapValueTextComponent.text = _unitTemplate.MaxSoldiers > 0 ? _unitTemplate.MaxSoldiers.ToString() : "-";
        speedValueTextComponent.text =  _unitTemplate.Speed > 0 ?  _unitTemplate.Speed.ToString() : "-";
    }
    
    public void SetEmpty()
    {
        //costResourceBlock.SetResources(new Resources());
        //maintenanceResourceBlock.SetResources(new Resources());
        speedValueTextComponent.text = "-";
        maxSoldierCapValueTextComponent.text = "-";
    }
}
