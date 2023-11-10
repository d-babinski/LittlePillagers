using UnityEngine;
public class ShipBuilder : MonoBehaviour
{
    [SerializeField] private Ship shipPrefab = null;
    
    public Ship BuildShip(UnitTemplate _template)
    {
        Ship _builtShip = Instantiate(shipPrefab);

        _builtShip.SpriteRenderer.sprite = _template.InGameSprite;
        _builtShip.Animator.runtimeAnimatorController = _template.Animator;
        _builtShip.SetSpeed(_template.Speed);
        _builtShip.SetSoldierCapacity(_template.MaxSoldiers);
        _builtShip.SetShipName(_template.UnitName);

        return _builtShip;
    }
}
