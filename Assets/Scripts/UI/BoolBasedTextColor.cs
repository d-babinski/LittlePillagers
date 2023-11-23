using TMPro;
using UnityEngine;

public class BoolBasedTextColor : MonoBehaviour
{
    [SerializeField] private bool activeOnTrue = true;
    [SerializeField] private BoolVariable trackedBool = null;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.white;

    private TextMeshProUGUI textComponent = null;
    private bool lastValue = false;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
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
        textComponent.color = trackedBool.Value == activeOnTrue ? activeColor : inactiveColor;
    }
}
