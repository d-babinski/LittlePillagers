using UnityEngine;

public class SoldierBuilder : MonoBehaviour
{
    [SerializeField] private Soldier shipPrefab = null;
    
    public Soldier CreateSoldier(SoldierTemplate _template)
    {
        Soldier _createdSoldier = Instantiate(shipPrefab);

        _createdSoldier.SpriteRenderer.sprite = _template.InGameSprite;
        _createdSoldier.Animator.runtimeAnimatorController = _template.Animator;
        _createdSoldier.SetAttack(_template.Attack);
        _createdSoldier.SetSoldierName(_template.SoldierName);
        _createdSoldier.SetCapacity(_template.Capacity);
        _createdSoldier.SetId(_template.SoldierId);

        return _createdSoldier;
    }
}
