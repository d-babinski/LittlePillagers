using System;
using UnityEngine;
using UnityEngine.UI;

public class BoolBasedImageColor : MonoBehaviour
{
    [SerializeField] private bool activeOnTrue = true;
    [SerializeField] private BoolVariable trackedBool = null;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.white;

    private Image buttonImage = null;
    private bool lastValue = false;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        adjustColor();
    }

    private void Update()
    {
        if (lastValue != trackedBool.Value)
        {
            lastValue = trackedBool.Value;
            adjustColor();
        }
    }

    private void adjustColor()
    {
        buttonImage.color = trackedBool.Value == activeOnTrue ? activeColor : inactiveColor;
    }
}
