using UnityEngine;

public class StageIcons : MonoBehaviour
{
    private Animator[] animators = null;

    public void SpawnStageIcons(Stage[] _stages)
    {
        animators = new Animator[_stages.Length];
        
        for (int i = 0; i < _stages.Length; i++)
        {
            Animator _spawnedAnimator = Instantiate(_stages[i].IngameIcon).GetComponent<Animator>();
            _spawnedAnimator.transform.parent = transform;
            _spawnedAnimator.transform.localPosition = new Vector2(10f/32f * i, 0f);
            animators[i] = _spawnedAnimator;
        }
    }

    public void OnStageBeaten(int _stage)
    {
        animators[_stage].SetTrigger("Destroy");
    }
}
