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
            case PlayerState.Preparation | PlayerState.Combat:
                _text.text = "Anytime";
                break;
            case PlayerState.Preparation:
                _text.text = "Preparation stage only";
                break;
            case PlayerState.Combat:
                _text.text = "Combat only";
                break;
            case 0:
                _text.text = "Passive";
                break;
        }
    }
}
