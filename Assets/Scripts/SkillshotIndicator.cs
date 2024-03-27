using UnityEngine;

public class SkillshotIndicator : MonoBehaviour
{
    [SerializeField] private PlayerSkillCaster trackedCaster = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    
    private void OnEnable()
    {
        trackedCaster.OnAimStartEvent += showIndicator;
        trackedCaster.OnAimEndEvent += hideIndicator;
    }
    
    private void showIndicator(Skill _skillBeingCast)
    {
        spriteRenderer.size = _skillBeingCast.AreaSize;
        spriteRenderer.enabled = true;
    }
    
    private void hideIndicator()
    {
        spriteRenderer.enabled = false;
    }

}
