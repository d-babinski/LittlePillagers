using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoldierStatBlock : MonoBehaviour
{
    [SerializeField] private ResourceBlockUI costResourceBlock = null;
    [SerializeField] private ResourceBlockUI maintenanceResourceBlock = null;
    [SerializeField] private TextMeshProUGUI attackValueTextComponent = null;
    [SerializeField] private TextMeshProUGUI capacityValueTextComponent = null;

    public void SetStatBlock(Resources _cost, Resources _maintenance, int _attack, int _capacity)
    {
        costResourceBlock.SetResources(_cost);
        maintenanceResourceBlock.SetResources(_maintenance);
        attackValueTextComponent.text = _attack > 0 ? _attack.ToString() : "-";
        capacityValueTextComponent.text = _capacity > 0 ? _capacity.ToString() : "-";
    }
}
