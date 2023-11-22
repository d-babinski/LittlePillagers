using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SkillCaster : MonoBehaviour
{
    public UnityEvent<Vector2> OnPrepareSkillshot = null;
    public UnityEvent OnSkillConfirmed = null;
    public UnityEvent OnSkillCanceled = null;
    
    [SerializeField] private ResourcesVariable playerResources = null;
    
    private Skill skillAwaitingConfirmation = null;

    
    public void TryUsingSkill(Skill _skillToUse)
    {
        Resources _requiredResources = new Resources(0, 0, 0, _skillToUse.GoldCost);

        if (playerResources.Value < _requiredResources)
        {
            return;
        }

        if (_skillToUse.Skillshot)
        {
            skillAwaitingConfirmation = _skillToUse;
            OnPrepareSkillshot?.Invoke(_skillToUse.AreaSize);
            return;
        }

        playerResources.Value -= _requiredResources;
        _skillToUse.UseSkill(Vector2.zero);
    }

    public void ConfirmUsingSkill()
    {
        if (skillAwaitingConfirmation == false)
        {
            return;
        }
        
        playerResources.Value -= new Resources(0, 0, 0, skillAwaitingConfirmation.GoldCost);
        skillAwaitingConfirmation.UseSkill(Camera.main.ScreenToWorldPoint(Mouse.current.position.value));
        skillAwaitingConfirmation = null;
        OnSkillConfirmed?.Invoke();
    }

    public void CancelSkill()
    {
        if (skillAwaitingConfirmation == false)
        {
            return;
        }
        
        skillAwaitingConfirmation = null;
        OnSkillCanceled?.Invoke();
    }
}
