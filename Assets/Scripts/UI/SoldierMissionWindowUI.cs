using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SoldierMissionWindowUI : MonoBehaviour
{
    public event Action OnSoldiersChosen = null;
    public int UnitsQuantity => unitSelectionPanel.UnitPanelCount;
    
    [SerializeField] private UnitsSelectionPanelUI unitSelectionPanel = null;
    [SerializeField] private TextMeshProUGUI totalResourceCapacityCountText = null;
    [SerializeField] private UnitCountText selectedUnitsCount = null;
    [FormerlySerializedAs("sendButton")][SerializeField] private ImageButton confirmButton = null;
    [SerializeField] private SidePanel panelController = null;

    private int maxTotalUnits = 0;
    private int[] unitCapacityValues = Array.Empty<int>();

    private void Awake()
    {
        confirmButton.OnButtonClicked += confirmSoldiers;
        unitSelectionPanel.OnValueChanged += updateTexts;
    }
    
    public void OpenWindow()
    {
        panelController.Open();
    }

    public void CloseWindow()
    {
        panelController.Close();
    }

    public void SetMaxUnits(int _maxUnits)
    {
        maxTotalUnits = _maxUnits;
        updateTexts();
    }

    public void SetUnitNames(string[] _names)
    {
        for (int i = 0; i < _names.Length; i++)
        {
            if (i < unitSelectionPanel.UnitPanelCount)
            {
                unitSelectionPanel.SetPanelName(i, _names[i]);
            }
        }
    }

    public void SetUnitQuantity(int[] _availability)
    {
        for (int i = 0; i < _availability.Length; i++)
        {
            if (i < unitSelectionPanel.UnitPanelCount)
            {
                unitSelectionPanel.SetPanelMaxUnits(i, _availability[i]);
            }
        }
    }
    
    public void SetUnitCapacities(int[] _capacities)
    {
        unitCapacityValues = _capacities;
        updateTexts();
    }

    public void ResetSelection()
    {
        unitSelectionPanel.ResetValues();
        updateTexts();
    }

    private void updateTexts()
    {
        selectedUnitsCount.UpdateCount(unitSelectionPanel.TotalSelectedUnits, maxTotalUnits);
        totalResourceCapacityCountText.text = getTotalResourceCapacity().ToString();
    }
    
    private int getTotalResourceCapacity()
    {
        int _totalCapacity = 0;
        
        for (int i = 0; i < unitSelectionPanel.UnitPanelCount; i++)
        {
            _totalCapacity += unitSelectionPanel.GetSelectedUnitsInPanel(i)*getUnitCapacity(i);
        }

        return _totalCapacity;
    }
    
    private int getUnitCapacity(int _id)
    {
        if (_id < 0 || _id >= unitCapacityValues.Length)
        {
            return 0;
        }

        return unitCapacityValues[_id];
    }

    private void confirmSoldiers()
    {
        if (isChoiceValid() == false)
        {
            return;
        }
        
        OnSoldiersChosen?.Invoke();
    }

    private bool isChoiceValid()
    {
        int _selectedUnits = unitSelectionPanel.TotalSelectedUnits;

         return _selectedUnits > 0 && _selectedUnits <= maxTotalUnits;
    }
    
    public void SetClosed()
    {
        panelController.CloseInstantly();
    }

    public int[] GetChosenQuantities()
    {
        return unitSelectionPanel.GetAllSelectedUnits();
    }
}
