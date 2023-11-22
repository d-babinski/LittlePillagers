using UnityEngine;

public class StageIcons : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController normalIconAnimatorController = null;
    [SerializeField] private RuntimeAnimatorController bossIconAnimatorController = null;

    private Animator[] animators = null;
    private float normalStageWidth = 0.2f;
    private float startingX = -0.5f;
    private float bossStageWidth = 0.35f;

    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>(true);
    }

    public void SetStages(Stage[] _stages)
    {
        float _currentX = startingX;

        for (int i = 0; i < animators.Length; i++)
        {
            if (i >= _stages.Length)
            {
                animators[i].gameObject.SetActive(false);
                continue;
            }

            if (i != 0)
            {
                _currentX += _stages[i].IsBoss ? bossStageWidth : normalStageWidth;
            }
            
            animators[i].transform.localPosition = new Vector2(_currentX, 0f);
            animators[i].gameObject.SetActive(true);
            animators[i].runtimeAnimatorController = _stages[i].IsBoss ? bossIconAnimatorController : normalIconAnimatorController;
            animators[i].SetTrigger("Restore");
        }
    }

    public void OnStageBeaten(int _stage)
    {
        animators[_stage].SetTrigger("Destroy");
    }
}
