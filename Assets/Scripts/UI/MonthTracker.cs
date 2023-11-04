using TMPro;
using UnityEngine;

public class MonthTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent = null;

    public void SetMonth(int _month)
    {
        textComponent.text = $"Month {_month}";
    }

}
