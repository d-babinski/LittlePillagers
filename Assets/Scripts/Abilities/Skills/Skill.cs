using UnityEngine;
using UnityEngine.Serialization;

public abstract class Skill : ScriptableObject
{
    [Header("Gameplay settings")]
    public PlayerState SkillAvailabilityDuringGameplayPhases = PlayerState.Preparation;
    [FormerlySerializedAs("Skillshot")] public bool IsSkillshot = false;

    [Header("Info")]
    public string SkillName = "";
    public string Description = "";
    
    [Header("Gold Cost")]
    public int GoldCost = 0;

    [Header("Optional if skillshot")]
    public Vector2 AreaSize = Vector2.one;

    public abstract void UseSkill(Vector2 _target);

    public bool IsUsableDuringPhase(PlayerState _state)
    {
        return SkillAvailabilityDuringGameplayPhases.HasFlag(_state);
    }
}
