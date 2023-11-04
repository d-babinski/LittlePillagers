using System;
using TMPro;
using UnityEngine;

public class UnitCountText : MonoBehaviour
{
    [SerializeField] private string prefix = String.Empty;

    private TextMeshProUGUI textComponent = null;
    private int available = 0;
    private int total = 0;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        updateText();
    }

    public void UpdateCount(int _available, int _total)
    {
        available = _available;
        total = _total;
        updateText();
    }
    
    private void updateText()
    {
        textComponent.text = $"{prefix}{available}/{total}";
    }

}
