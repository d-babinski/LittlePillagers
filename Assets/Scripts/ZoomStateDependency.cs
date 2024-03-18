using UnityEngine;

public class ZoomStateDependency : MonoBehaviour
{
    [SerializeField] private ZoomStateVariable zoomStateVariable = null;
    [SerializeField] private GameObject controlledObject = null;
    [SerializeField] private bool requiresZoomIn = false;
    
    private void Start()
    { 
        setGameObjectStateForCurrentZoomState(zoomStateVariable);
        zoomStateVariable.ZoomStateChangedEvent += onZoomStateChanged;
    }

    private void OnDestroy()
    {
        zoomStateVariable.ZoomStateChangedEvent-= onZoomStateChanged;
    }

    private void onZoomStateChanged()
    {
        setGameObjectStateForCurrentZoomState(zoomStateVariable);
    }

    private void setGameObjectStateForCurrentZoomState(ZoomStateVariable _zoomState)
    {
        if (_zoomState.IsZoomed != requiresZoomIn)
        {
            controlledObject.SetActive(false);
            return;
        }

        controlledObject.SetActive(true);
    }

    public void SetStateDependancy(GameObject _controlledGameObject, ZoomStateVariable _zoomStateVariable, bool _requiresZoomIn)
    {
        zoomStateVariable = _zoomStateVariable;
        requiresZoomIn = _requiresZoomIn;
        controlledObject = _controlledGameObject;
    }
}

