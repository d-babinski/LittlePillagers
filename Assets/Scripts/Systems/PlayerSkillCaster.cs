using System;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSkillCaster : ScriptableObject
{
    public event Action<Skill> OnAimStartEvent = null;
    public event Action OnAimEndEvent = null;
    public event Action<Skill> OnSkillCastEvent = null;
    
    public bool IsAiming => currentState == CastingState.Aiming;

    private enum CastingState
    {
        None = 0,
        Aiming = 1,
    }
    
    [SerializeField] private SkillVariable[] playerSkills = null;

    private CastingState currentState = CastingState.None;
    
    private Skill currentlyAimedSkill = null;

    public void CastSkill(Skill _skillToCast)
    {
        if (_skillToCast.IsSkillshot == true)
        {
            currentlyAimedSkill = _skillToCast;
            currentState = CastingState.Aiming;
            OnAimStartEvent?.Invoke(_skillToCast);
            return;
        }

        if (IsAiming == true)
        {
            CancelCast();
        }
        
        _skillToCast.UseSkill(Vector2.zero);
        OnSkillCastEvent?.Invoke(_skillToCast);
    }

    public void ConfirmSkillshot(Vector2 _target)
    {
        currentlyAimedSkill.UseSkill(_target);
        currentState = CastingState.None;
        OnSkillCastEvent?.Invoke(currentlyAimedSkill);
        OnAimEndEvent?.Invoke();
        currentlyAimedSkill = null;
    }

    public void CastSkill(int _skillSlot)
    {
        CastSkill(playerSkills[_skillSlot].Value);
    }

    public void CancelCast()
    {
        if (IsAiming == false)
        {
            return;
        }

        currentState = CastingState.None;
        currentlyAimedSkill = null;
        OnAimEndEvent?.Invoke();
    }

    public bool CanAfford(int _slotId, Resources _resources)
    {
        return _resources.Gold >= playerSkills[_slotId].Value.GoldCost;
    }
}
