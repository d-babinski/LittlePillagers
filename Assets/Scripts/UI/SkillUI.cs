using UnityEngine;
using UnityEngine.Events;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private UnityEvent<Skill> skillUsedActions = null;
    [SerializeField] private SkillVariable assignedSkill = null;
    [SerializeField] private CanvasGroup buttonCanvasGroup = null;

    private bool interactable = true;

    private void Start()
    {
        UpdateButtonForPreparationStage();
    }

    public void UseSkill()
    {
        if (interactable == false)
        {
            return;
        }
        
        skillUsedActions?.Invoke(assignedSkill.Value);
    }

    public void UpdateButtonForPreparationStage()
    {
        changeInteraction(assignedSkill.Value.UsableDuringPrepare);
    }

    public void UpdateButtonForCombatStage()
    {
        changeInteraction(assignedSkill.Value.UsableDuringFight);
    }

    private void changeInteraction(bool _isInteractable)
    {
        if (interactable == _isInteractable)
        {
            return;
        }
        
        interactable = _isInteractable;
        buttonCanvasGroup.alpha = _isInteractable ? 1f : 0.5f;
        buttonCanvasGroup.blocksRaycasts = _isInteractable;
    }
}
