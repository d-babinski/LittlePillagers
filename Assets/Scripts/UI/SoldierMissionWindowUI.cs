using System;
using TMPro;
using UnityEngine;

public class SoldierMissionWindowUI : MonoBehaviour
{
    public event Action OnSoldiersChosen = null;
    public int NumberOfPanels => unitSelectionPanel.UnitPanelCount;

    [SerializeField] private UnitsSelectionPanelUI unitSelectionPanel = null;
    [SerializeField] private TextMeshProUGUI totalResourceCapacityCountText = null;
    [SerializeField] private Window panelController = null;

    private int maxTotalUnits = 0;
    private UnitTemplate[] soldiers = Array.Empty<UnitTemplate>();

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

    public void SetUnitNames(int _id, string _name)
    {
        unitSelectionPanel.SetPanelName(_id, _name);
    }

    public void SetUnitQuantity(int _id, int _maxUnits)
    {
        unitSelectionPanel.SetPanelMaxUnits(_id, _maxUnits);
    }

    public void SetUnitData(UnitTemplate[] _templates)
    {
        soldiers = _templates;
        
        for (int i = 0; i < NumberOfPanels; i++)
        {
            if (i >= soldiers.Length)
            {
                return;
            }
            
            unitSelectionPanel.SetPanelName(i,_templates[i].UnitName);
        }
        
        updateTexts();
    }

    public void ResetSelection()
    {
        unitSelectionPanel.ResetValues();
        updateTexts();
    }

    private void updateTexts()
    {
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
        if (_id < 0 || _id >= soldiers.Length)
        {
            return 0;
        }

        return soldiers[_id].Capacity;
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
