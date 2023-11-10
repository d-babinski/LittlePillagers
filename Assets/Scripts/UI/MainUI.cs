using DG.Tweening;
using System;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private ManagementWindow managementWindow = null; 
 
    private Tween uiFadeTween = null;

    public void Show()
    {
        uiFadeTween?.Kill();
        uiFadeTween = canvasGroup.DOFade(1f, 0.5f).SetUpdate(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    
    public void Hide()
    {
        uiFadeTween?.Kill();
        uiFadeTween = canvasGroup.DOFade(0f, 0.5f).SetUpdate(true);
        managementWindow.CloseAllWindowswithoutTransition();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void ToggleShipWindow()
    {
        managementWindow.ToggleShipWindow();
    }

    public void ToggleSoldierWindow()
    {
        managementWindow.ToggleSoldierWindow();
    }
    
    public void CloseAllWindows()
    {
        managementWindow.CloseAllWindows();
    }
}
