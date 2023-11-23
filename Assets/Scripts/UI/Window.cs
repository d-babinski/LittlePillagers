using DG.Tweening;
using UnityEngine;

public class Window : MonoBehaviour
{
    private enum PanelState
    {
        Closed = 0,
        Opened = 1,
    }

    [SerializeField] private float openedPosAnchoredX = 0f; 
    [SerializeField] private float closedPosAnchoredX = 0f;
    [SerializeField] private float tweenTime = 1f;
    
    private PanelState currentPanelState = PanelState.Closed;
    private Tween panelMoveTween = null;
    private RectTransform rectTransform = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        disablePanel();
        rectTransform.anchoredPosition = new Vector2(closedPosAnchoredX, rectTransform.anchoredPosition.y);
    }

    public void ToggleOpen()
    {
        if (IsOpen())
        {
            Close();
            return;
        }
        
        Open();
    }

    public bool IsOpen()
    {
        return currentPanelState == PanelState.Opened;
    }

    public void Open()
    {
        currentPanelState = PanelState.Opened;
        panelMoveTween?.Kill();
        
        enablePanel();
        panelMoveTween = rectTransform.DOAnchorPosX(openedPosAnchoredX, tweenTime).SetUpdate(true);
    }

    public void Close()
    {
        currentPanelState = PanelState.Closed;
        panelMoveTween?.Kill();
        
        panelMoveTween = rectTransform.DOAnchorPosX(closedPosAnchoredX, tweenTime).SetUpdate(true).OnComplete(disablePanel);
    }

    public void CloseInstantly()
    {
        panelMoveTween?.Kill();
        currentPanelState = PanelState.Closed;
        rectTransform.anchoredPosition = new Vector2(closedPosAnchoredX, rectTransform.anchoredPosition.y);
    }

    private void enablePanel()
    {
        transform.GetChild(0).gameObject.SetActive(true);   
    }
    
    private void disablePanel()
    {
        transform.GetChild(0).gameObject.SetActive(false);   
    }
}
