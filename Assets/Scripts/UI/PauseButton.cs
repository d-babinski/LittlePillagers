using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : ImageButton
{
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.white;

    private Image buttonImage = null;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SetActiveColor()
    {
        buttonImage.color = activeColor;
    }

    public void SetInactiveColor()
    {
        buttonImage.color = inactiveColor;
    }
}
