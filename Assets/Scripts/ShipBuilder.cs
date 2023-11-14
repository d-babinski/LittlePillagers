using UnityEngine;

[CreateAssetMenu]
public class ShipBuilder : UnitBuilder
{
    [SerializeField] private Ship shipPrefab = null;
    
    public override GameObject Build(UnitTemplate _template)
    {
        Ship _builtShip = Instantiate(shipPrefab);

        _builtShip.SpriteRenderer.sprite = _template.InGameSprite;
        _builtShip.Animator.runtimeAnimatorController = _template.Animator;
        _builtShip.UnitType = _template.TypeOfUnit;
        _builtShip.ShipName = _template.UnitName;
        _builtShip.Seats = _template.MaxSoldiers;
        _builtShip.GetComponent<Navigator>().Speed = _template.Speed;
        //_builtShip.SetShipName(_template.UnitName);

        return _builtShip.gameObject;
    }
}
