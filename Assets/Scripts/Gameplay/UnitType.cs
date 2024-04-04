using System;
using UnityEngine;

[CreateAssetMenu]
public class UnitType : ScriptableObject
{
    [Header("Instantiating")]
    [SerializeField] private Unit unitPrefab = null;
    
    [Header("Visuals")]
    public Sprite PreviewIcon = null;
    public Sprite InGameSprite = null;
    public RuntimeAnimatorController Animator = null;
    
    [Space(10f)]
    [Header("Common Stats")]
    public string UnitName = "";
    public Resources BaseCost = new Resources();
    public int Speed = 10;
    public int MaxHp = 10;

    [Space(10f)]
    [Header("Attacks")]
    public Attack[] Attacks = Array.Empty<Attack>();
    
    public Unit InstantiateUnit()
    {
        Unit _createdUnit = Instantiate(unitPrefab);
        _createdUnit.UnitType = this;
        return _createdUnit;
    }
}