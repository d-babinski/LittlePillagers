using UnityEngine;
using UnityEngine.Events;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private PlayerSkillCaster skillCaster = null;
    [SerializeField] private UnityEvent<Skill> skillUsedActions = null;
    [SerializeField] private SkillVariable assignedSkill = null;
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private CanvasGroup buttonCanvasGroup = null;

    private bool interactable = true;

    private void Update()
    {
        bool _canAfford = playerResources.Value.Gold >= assignedSkill.Value.GoldCost;
        bool _shouldBeInteractable = _canAfford && assignedSkill.Value.IsUsableDuringPhase(playerStateVariable.CurrentState);

        if (_shouldBeInteractable != interactable)
        {
            interactable = _shouldBeInteractable;
            applyVisuals(_shouldBeInteractable);
        }
    }

    public void UseSkill()
    {
        if (interactable == false)
        {
            return;
        }
        
        skillUsedActions?.Invoke(assignedSkill.Value);
    }

    private void applyVisuals(bool _isInteractable)
    {
        buttonCanvasGroup.alpha = _isInteractable ? 1f : 0.5f;
        buttonCanvasGroup.blocksRaycasts = _isInteractable;
    }
}
