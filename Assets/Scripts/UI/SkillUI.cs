using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillUI : MonoBehaviour
{
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private PlayerSkillCaster playerSkillCaster = null;
    [SerializeField] private SkillVariable assignedSkill = null;
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private CanvasGroup buttonCanvasGroup = null;

    private bool interactable = true;
    private Button button = null;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        bool _canAfford = playerResources.Value.Gold >= assignedSkill.Value.GoldCost;
        bool _shouldBeInteractable = _canAfford && assignedSkill.Value.IsUsableDuringPhase(playerStateVariable.CombatState);

        if (_shouldBeInteractable != interactable)
        {
            interactable = _shouldBeInteractable;
            button.interactable = interactable;
        }
    }

    public void UseSkill()
    {
        if (interactable == false)
        {
            return;
        }
        
        playerSkillCaster.CastSkill(assignedSkill.Value);
    }

    private void applyVisuals(bool _isInteractable)
    {
        buttonCanvasGroup.alpha = _isInteractable ? 1f : 0.5f;
        buttonCanvasGroup.blocksRaycasts = _isInteractable;
    }
}
