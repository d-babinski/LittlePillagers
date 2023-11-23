using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("Gameplay settings")]
    public bool UsableDuringPrepare = false;
    public bool UsableDuringFight = false;
    public bool Skillshot = false;

    [Header("Info")]
    public string SkillName = "";
    public string Description = "";
    
    [Header("Gold Cost")]
    public int GoldCost = 0;

    [Header("Optional if skillshot")]
    public Vector2 AreaSize = Vector2.one;

    public abstract void UseSkill(Vector2 _target);
}
