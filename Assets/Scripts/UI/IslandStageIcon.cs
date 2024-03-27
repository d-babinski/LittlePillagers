using UnityEngine;

public class IslandStageIcon : MonoBehaviour
{
    [SerializeField] private Vector2 offsetFromIsland = Vector2.up;
    [SerializeField] private Vector2 offsetBetweenIcons = Vector2.right * 0.25f;
    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private int trackedStageNumber = 0;
    private Island trackedIsland = null;
    private bool isDestroyed = false;
    
    public void Initialize(Island _islandToTrack, int _stageNumber)
    {
        Stage _stage = _islandToTrack.GetStageSetup(_stageNumber);
        
        trackedIsland = _islandToTrack;
        trackedStageNumber = _stageNumber;
        animator.runtimeAnimatorController = _stage.StageIconAnimator;
        spriteRenderer.sprite = _stage.StageIcon;
        
        Vector2 _firstIconPos = -offsetBetweenIcons* (trackedIsland.TotalStageCount/2);
        Vector2 _thisIconNumberPos = _firstIconPos + offsetBetweenIcons*_stageNumber;
        Vector2 _totalOffset = offsetFromIsland + _thisIconNumberPos;

        transform.position = (Vector2)_islandToTrack.transform.position + _totalOffset;

        if (_islandToTrack.CurrentStageNumber > trackedStageNumber)
        {
            setToDestroyed();
        }

        trackedIsland.OnStageChange += OnTrackedIslandStageChange;
    }

    private void OnDestroy()
    {
        if (trackedIsland == null)
        {
            return;
        }

        trackedIsland.OnStageChange -= OnTrackedIslandStageChange;
    }

    private void OnTrackedIslandStageChange(int _newStage)
    {
        if (isDestroyed == true && _newStage < trackedStageNumber)
        {
            setToRestored();
            return;
        }

        if (isDestroyed == false && _newStage > trackedStageNumber)
        {
            playDestroy();
            return;
        }
    }

    private void playDestroy()
    {
        animator.SetTrigger("Destroy");
    }

    private void setToDestroyed()
    {
        animator.Play("Destroyed");
    }

    private void setToRestored()
    {
        animator.SetTrigger("Restore");
    }
}
