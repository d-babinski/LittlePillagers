using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Skillshot")]
public class CastSkillshot : Skill
{
    [SerializeField] private GameObject prefabToSpawn = null;
    
    public override void UseSkill(Vector2 _target)
    {
        Instantiate(prefabToSpawn, _target, Quaternion.identity);
    }
}
