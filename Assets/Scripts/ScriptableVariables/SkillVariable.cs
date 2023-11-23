using UnityEngine;

[CreateAssetMenu(menuName = "Variables/SkillVariable")]
public class SkillVariable : ScriptableObject
{
    [SerializeField] private Skill value = null;

    public Skill Value
    {
        get { return value; }
        set { this.value = value;}
    }
}
