using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private UnitDatabase unitDatabase = null;
    [SerializeField] private UnitTemplateDatabase templateDatabase = null;

    public void BuyUnits(UnitType _type)
    {
        Resources _unitCost = templateDatabase.GetTemplate(_type).BaseCost;
        
        if (playerResources.Value < _unitCost)
        {
            return;
        }
        
        unitDatabase.AddUnits(_type);
        playerResources.Value -= _unitCost;
    }
}
