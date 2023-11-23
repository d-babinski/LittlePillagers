using UnityEngine;
using UnityEngine.Events;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private UnityEvent<Skill> skillUsedActions = null;
    [SerializeField] private SkillVariable assignedSkill = null;
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private CanvasGroup buttonCanvasGroup = null;

    private bool interactable = true;
    private bool fitsCurrentStage = true;

    private void Start()
    {
        UpdateButtonForPreparationStage();
    }

    private void Update()
    {
        bool _canAfford = playerResources.Value.Gold >= assignedSkill.Value.GoldCost;
        bool _shouldBeInteractable = _canAfford && fitsCurrentStage;

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

    public void UpdateButtonForPreparationStage()
    {
        fitsCurrentStage = assignedSkill.Value.UsableDuringPrepare;
    }

    public void UpdateButtonForCombatStage()
    {
       fitsCurrentStage = assignedSkill.Value.UsableDuringFight;
    }

    private void applyVisuals(bool _isInteractable)
    {
        buttonCanvasGroup.alpha = _isInteractable ? 1f : 0.5f;
        buttonCanvasGroup.blocksRaycasts = _isInteractable;
    }
}
