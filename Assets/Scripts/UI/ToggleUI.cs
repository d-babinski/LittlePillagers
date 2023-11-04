using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public event Action<ToggleUI> OnToggleStateChanged= null;
    public bool IsToggled => isToggled;

    [SerializeField] private Image toggledImage = null;
    [SerializeField] private ImageButton button = null;
    
    private bool isToggled = false;

    private void Awake()
    {
        SetToggleState(isToggled);
        button.OnButtonClicked += onToggleClicked;
    }
    
    private void onToggleClicked()
    {
        SetToggleState(!isToggled);
        OnToggleStateChanged?.Invoke(this);
    }

    public void SetToggleState(bool _newState)
    {
        isToggled = _newState;
        toggledImage.enabled = _newState;
    }
}
