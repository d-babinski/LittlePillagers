using UnityEngine;

public class StageIcon : MonoBehaviour
{
    public Stage ReferencedStage = null;

    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private bool isBeaten = false;
    private Stage cachedStage = null;

    private void Update()
    {
        if (cachedStage != ReferencedStage)
        {
            updateStage(ReferencedStage);
            cachedStage = ReferencedStage;
        }

        if (ReferencedStage == null)
        {
            return;
        }

        if (isBeaten != ReferencedStage.IsBeaten)
        {
            isBeaten = ReferencedStage.IsBeaten;
            animator.SetTrigger(isBeaten == true ? "Destroy" : "Restore");
        }
    }

    private void updateStage(Stage _newStage)
    {
        if (_newStage == null)
        {
            spriteRenderer.sprite = null;
            animator.runtimeAnimatorController = null;
            return;
        }

        spriteRenderer.sprite = _newStage.StageIcon;
        
        animator.runtimeAnimatorController = _newStage.StageIconAnimator;
        animator.SetTrigger(_newStage.IsBeaten == true ? "Destroy" : "Restore");
    }
}
