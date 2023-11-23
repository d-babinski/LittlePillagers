using DG.Tweening;
using UnityEngine;

public class VisibilityTween : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private FloatVariable alphaTweenTime = null;

    private Tween alphaTween = null;

    public void Show()
    {
        alphaTween?.Kill();
        alphaTween = canvasGroup.DOFade(1f, alphaTweenTime.Value).SetUpdate(true);
    }

    public void ShowDelayed(float _delay)
    {
        alphaTween?.Kill();
        alphaTween = DOVirtual.DelayedCall(_delay, Show);
    }
    
    public void Hide()
    {
        alphaTween?.Kill();
        alphaTween = canvasGroup.DOFade(0f, alphaTweenTime.Value).SetUpdate(true);
    }
}
