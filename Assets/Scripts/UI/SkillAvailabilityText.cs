using TMPro;
using UnityEngine;

public class SkillAvailabilityText : MonoBehaviour
{
    private enum SkillAvailability{Passive = 0, PreparationOnly = 1, CombatOnly = 2, Anytime = 3}
    
    [SerializeField] private SkillVariable assignedSkill = null;
    [SerializeField] private TextMeshProUGUI textToSet = null;

    private Skill lastSkill = null;
    
    private void Update()
    {
        if (lastSkill == assignedSkill.Value)
        {
            return;
        }
        
        setTextContentBasedOnAvailability(textToSet, getCurrentSkillAvailability(assignedSkill.Value));
        lastSkill = assignedSkill.Value;
    }

    private SkillAvailability getCurrentSkillAvailability(Skill _skill)
    {
        if (_skill.UsableDuringFight == false && _skill.UsableDuringPrepare == false)
        {
            return SkillAvailability.Passive;
        }
        
        if (_skill.UsableDuringFight == false)
        {
            return SkillAvailability.PreparationOnly;
        }

        if (_skill.UsableDuringPrepare == false)
        {
            return SkillAvailability.CombatOnly;
        }

        return SkillAvailability.Anytime;
    }

    private void setTextContentBasedOnAvailability(TextMeshProUGUI _text, SkillAvailability _availability)
    {
        switch (_availability)
        {
            case SkillAvailability.Anytime:
                _text.text = "Anytime";
                break;
            case SkillAvailability.PreparationOnly:
                _text.text = "Preparation stage only";
                break;
            case SkillAvailability.CombatOnly:
                _text.text = "Combat only";
                break;
            case SkillAvailability.Passive:
                _text.text = "Passive";
                break;
        }
    }
}
