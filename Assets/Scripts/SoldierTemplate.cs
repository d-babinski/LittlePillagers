using UnityEngine;

[CreateAssetMenu(fileName = "warrior_default", menuName = "Templates/Soldier")]
public class SoldierTemplate : ScriptableObject
{
    public int SoldierId = 0;
    public Resources BaseCost = new Resources();
    public Resources Maintenance = new Resources();
    public int Attack = 3;
    public int Capacity = 10;
    public Sprite Portrait = null;
    public Sprite InGameSprite = null;
    public string SoldierName = "";
    public RuntimeAnimatorController Animator = null;
}
