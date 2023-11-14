using System;
using TMPro;
using UnityEngine;

public class MissionUnitUI : MonoBehaviour
{
    public int CurrentlyChosen => currentlyChosen;
    
    [SerializeField] private ImageButton plusButton = null;
    [SerializeField] private ImageButton minusButton = null;

    [SerializeField] private TextMeshProUGUI maxUnitsAvailableText = null;
    [SerializeField] private TextMeshProUGUI currentlyChosenUnitsText = null;

    [SerializeField] private TextMeshProUGUI unitNameText = null;
    
    private int maxUnits = 0;
    private int currentlyChosen = 0;

    private void Awake()
    {
        currentlyChosenUnitsText.text = currentlyChosen.ToString();
        maxUnitsAvailableText.text = maxUnits.ToString();
    }

    private void onPlusButtonClicked()
    {
        currentlyChosen = Mathf.Clamp(currentlyChosen + 1, 0, maxUnits);
        currentlyChosenUnitsText.text = currentlyChosen.ToString();
    }
    
    private void onMinusButtonClicked()
    {
        currentlyChosen = Mathf.Clamp(currentlyChosen - 1, 0, maxUnits);
        currentlyChosenUnitsText.text = currentlyChosen.ToString();
    }

    public void ResetCounter()
    {
        currentlyChosen = 0;
        currentlyChosenUnitsText.text = currentlyChosen.ToString();
    }

    public void SetUnitName(string _name)
    {
        unitNameText.text = _name;
    }

    public void SetMaxUnitsAvailable(int _newMaxUnits)
    {
        maxUnits = _newMaxUnits;
        maxUnitsAvailableText.text = maxUnits.ToString();
        
        currentlyChosen = Mathf.Clamp(currentlyChosen, 0, maxUnits);
        currentlyChosenUnitsText.text = currentlyChosen.ToString();
    }
}
