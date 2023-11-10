using UnityEngine;
public class UnitTemplate : ScriptableObject
{
    public UnitType TypeOfUnit =null;
    public Sprite PreviewIcon = null;
    public Sprite InGameSprite = null;
    public RuntimeAnimatorController Animator = null;

    public string UnitName = "";
    public Resources BaseCost = new Resources();
    public Resources Maintenance = new Resources();
    public int Attack = 3;
    public int Capacity = 10;
    public int MaxSoldiers = 3;
    public int Speed = 10;
}
