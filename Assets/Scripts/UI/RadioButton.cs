using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadioButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnRadioButtonClicked = null;
    public bool IsSelected => isSelected;

    [SerializeField] private Image toggledImage = null;

    private bool isSelected = false;

    public void SelectRadioButton()
    {
        isSelected = true;
        toggledImage.enabled = true;
    }

    public void UnselectRadioButton()
    {
        isSelected = false;
        toggledImage.enabled = false;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SelectRadioButton();
        OnRadioButtonClicked?.Invoke();
    }
}
