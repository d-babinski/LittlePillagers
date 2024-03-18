using TMPro;
using UnityEngine;

public class SkillAvailabilityText : MonoBehaviour
{
    [SerializeField] private SkillVariable assignedSkill = null;
    [SerializeField] private TextMeshProUGUI textToSet = null;

    private Skill lastSkill = null;
    
    private void Update()
    {
        if (lastSkill == assignedSkill.Value)
        {
            return;
        }
        
        setTextContentBasedOnAvailability(textToSet, assignedSkill.Value);
        lastSkill = assignedSkill.Value;
    }

    private void setTextContentBasedOnAvailability(TextMeshProUGUI _text, Skill _skill)
    {
        switch (_skill.SkillAvailabilityDuringGameplayPhases)
        {
            case PlayerCombatState.Preparation | PlayerCombatState.Combat:
                _text.text = "Anytime";
                break;
            case PlayerCombatState.Preparation:
                _text.text = "Preparation stage only";
                break;
            case PlayerCombatState.Combat:
                _text.text = "Combat only";
                break;
            case 0:
                _text.text = "Passive";
                break;
        }
    }
}
