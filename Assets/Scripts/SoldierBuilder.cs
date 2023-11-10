using UnityEngine;

public class SoldierBuilder : MonoBehaviour
{
    [SerializeField] private Soldier shipPrefab = null;
    
    public Soldier CreateSoldier(UnitTemplate _template)
    {
        Soldier _createdSoldier = Instantiate(shipPrefab);

        _createdSoldier.SpriteRenderer.sprite = _template.InGameSprite;
        _createdSoldier.Animator.runtimeAnimatorController = _template.Animator;
        _createdSoldier.SetAttack(_template.Attack);
        _createdSoldier.SetSoldierName(_template.UnitName);
        _createdSoldier.SetCapacity(_template.Capacity);

        return _createdSoldier;
    }
}
