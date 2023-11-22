using TMPro;
using UnityEngine;

public class SkillNameText : MonoBehaviour
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

        textToSet.text = assignedSkill.Value.SkillName;
        lastSkill = assignedSkill.Value;
    }
}
