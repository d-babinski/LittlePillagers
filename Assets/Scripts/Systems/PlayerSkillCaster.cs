using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSkillCaster : ScriptableObject
{
    public event Action<Skill> OnAimStart = null;
    public event Action OnAimEnd = null;
    
    public bool IsCasting => currentState == CastingState.Aiming;

    private enum CastingState
    {
        None = 0,
        Aiming = 1,
    }
    
    [SerializeField] private SkillVariable[] playerSkills = null;

    private CastingState currentState = CastingState.None;
    
    private Skill currentlyAimedSkill = null;
    
    public void CastSkill(int _skillSlot)
    {
        Skill _skillToCast = playerSkills[_skillSlot].Value;

        if (_skillToCast.IsSkillshot == true)
        {
            currentlyAimedSkill = _skillToCast;
            currentState = CastingState.Aiming;
            OnAimStart?.Invoke(_skillToCast);
        }
    }

    public void CancelCast()
    {
        if (IsCasting == false)
        {
            return;
        }

        currentState = CastingState.None;
        currentlyAimedSkill = null;
        OnAimEnd?.Invoke();
    }

    public bool CanAfford(int _slotId, Resources _resources)
    {
        return _resources.Gold >= playerSkills[_slotId].Value.GoldCost;
    }
}
