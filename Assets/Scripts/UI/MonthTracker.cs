using System;
using TMPro;
using UnityEngine;

public class MonthTracker : MonoBehaviour
{
    [SerializeField] private IntVariable dataSource = null;
    [SerializeField] private TextMeshProUGUI textComponent = null;

    private void Update()
    {
        setMonth(dataSource.Value);
    }

    private void setMonth(int _month)
    {
        textComponent.text = $"Month {_month}";
    }

}
