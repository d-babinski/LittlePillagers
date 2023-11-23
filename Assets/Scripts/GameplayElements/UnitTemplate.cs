using UnityEngine;

[CreateAssetMenu]
public class UnitTemplate : ScriptableObject
{
    [Header("Visuals")]
    public Sprite PreviewIcon = null;
    public Sprite InGameSprite = null;
    public RuntimeAnimatorController Animator = null;
    
    [Space(10f)]
    [Header("Builder")]
    public UnitBuilder InstanceBuilder = null;
    
    [Space(10f)]
    [Header("Common Stats")]
    public string UnitName = "";
    public UnitType TypeOfUnit =null;
    public Resources BaseCost = new Resources();
    public int Speed = 10;
    public int MaxHp = 10;
    
    [Space(10f)]
    [Header("Melee Attack")]
    public Attack MeleeAttack = null;
    public int MeleeDamage = 3;
    [Header("Ranged Attack")]
    public Attack RangedAttack = null;
    public int RangedDamage = 3;
    [Header("Special Attack")]
    public Attack SpecialAttack = null;
    public int SpecialDamage = 3;
    public float SpecialAttCooldown = 10f;
    
    public Unit InstantiateObject()
    {
        return InstanceBuilder.Build(this).GetComponent<Unit>();
    }
}