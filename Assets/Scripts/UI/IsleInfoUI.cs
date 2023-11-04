
using DG.Tweening;
using UnityEngine;

public class IsleInfoUI : MonoBehaviour
{
    private Resources maxResourcesPerPop = new Resources(20,20,20,20);
    
    [SerializeField] private IsleNameplateUI nameplate = null;
    [SerializeField] private IsleStatsUI stats = null;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private float alphaTweenTime = 0.25f;

    private Tween alphaTween = null;
    private RectTransform rectTransform = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ShowAt(Vector3 _pos)
    {
        rectTransform.anchoredPosition = _pos;
        Show();
    }

    public void Show()
    {
        alphaTween?.Kill();
        alphaTween = canvasGroup.DOFade(1f, alphaTweenTime).SetUpdate(true);
    }

    public void SetIsleInfo(string _name, Resources _currentResources, int _population)
    {
        nameplate.SetNameplate(_name);
        stats.SetResources(_currentResources, maxResourcesPerPop * _population);
        stats.SetPopulation(_population);
    }

    public void Hide()
    {
        alphaTween?.Kill();
        alphaTween = canvasGroup.DOFade(0f, alphaTweenTime).SetUpdate(true);
    }
    
    public void RefreshIsleData(Resources _currentResources, int _population)
    {
        stats.SetResources(_currentResources, maxResourcesPerPop * _population);
        stats.SetPopulation(_population);
    }
}
