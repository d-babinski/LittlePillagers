using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceBlockColoring : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color negativeValueColor = Color.red;
    
    [SerializeField] private TextMeshProUGUI woodCountTextComponent = null;
    [SerializeField] private TextMeshProUGUI wheatCountTextComponent = null;
    [SerializeField] private TextMeshProUGUI metalCountTextComponent = null;
    [SerializeField] private TextMeshProUGUI goldCountTextComponent = null;

    public void SetColoringBasedOnResources(Resources _resources)
    {
        setTextColorByValue(woodCountTextComponent, _resources.Wood);
        setTextColorByValue(wheatCountTextComponent, _resources.Wheat);
        setTextColorByValue(metalCountTextComponent, _resources.Metal);
        setTextColorByValue(goldCountTextComponent, _resources.Gold);
    }

    private void setTextColorByValue(TextMeshProUGUI _text , int _value)
    {
        _text.color = _value < 0 ? negativeValueColor : defaultColor;
    }
}
