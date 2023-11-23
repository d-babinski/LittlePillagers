using System;
using TMPro;
using UnityEngine;

public class UnitCountText : MonoBehaviour
{
    [SerializeField] private IntVariable availableUnits = null;
    [SerializeField] private IntVariable totalUnits = null;
    [SerializeField] private string prefix = String.Empty;

    private TextMeshProUGUI textComponent = null;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        updateText();
    }

    private void updateText()
    {
        textComponent.text = $"{availableUnits.Value}/{totalUnits.Value}";
    }

}
