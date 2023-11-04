using UnityEngine;

[CreateAssetMenu(fileName = "ship_default", menuName = "Templates/Ship")]
public class ShipTemplate : ScriptableObject
{
    public int ShipId = 0;
    public Resources BaseCost = new Resources();
    public Resources Maintenance = new Resources();
    public int MaxSoldiers = 3;
    public int Speed = 10;
    public Sprite PreviewIcon = null;
    public Sprite InGameSprite = null;
    public RuntimeAnimatorController Animator = null;
    public string ShipName = "Galley";
}