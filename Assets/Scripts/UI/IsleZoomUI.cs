using DG.Tweening;
using System;
using UnityEngine;

public class IsleZoomUI : MonoBehaviour
{
    private const float DELAY_BEFORE_SHOWING_ISLE_STATS = 2f;
    public bool AllWindowsClosed => attackMenu.IsClosed;

    [SerializeField] private AttackMenuUI attackMenu = null;
    [SerializeField] private VisibilityTween isleInfo = null;
    [SerializeField] private IsleZoomCamera zoomCam = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private Tween isleInfoVisibilityTween = null;

    public void Show()
    {
        zoomCam.Show();
        attackMenu.Enable();

        isleInfoVisibilityTween = DOVirtual.DelayedCall(DELAY_BEFORE_SHOWING_ISLE_STATS, _showIsleInfo).SetUpdate(true);

        void _showIsleInfo()
        {
            isleInfo.Show();
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Hide()
    {
        isleInfoVisibilityTween?.Kill();
        canvasGroup.blocksRaycasts = false;
        isleInfo.Hide();
        zoomCam.Hide();
        attackMenu.Disable();
    }
    
    public void Back()
    {
        attackMenu.Back();
    }
    
    public void OpenAttackPanel()
    {
        attackMenu.OpenAttackPanel();
    }
}
