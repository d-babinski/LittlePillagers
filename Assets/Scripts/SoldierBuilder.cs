using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class SoldierBuilder : UnitBuilder
{
    [FormerlySerializedAs("shipPrefab")][SerializeField] private Soldier soldierPrefab = null;
    
    public override GameObject Build(UnitTemplate _template)
    {
        Soldier _createdSoldier = Instantiate(soldierPrefab);

        _createdSoldier.SpriteRenderer.sprite = _template.InGameSprite;
        _createdSoldier.UnitType = _template.TypeOfUnit;
        _createdSoldier.Animator.runtimeAnimatorController = _template.Animator;
        _createdSoldier.Attack = _template.Attack;
        _createdSoldier.SoldierName = (_template.UnitName);
        _createdSoldier.Capacity = (_template.Capacity);

        return _createdSoldier.gameObject;
    }
}
