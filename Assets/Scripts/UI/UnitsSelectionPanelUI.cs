using System;
using UnityEngine;

public class UnitsSelectionPanelUI : MonoBehaviour
{
    public int UnitPanelCount => unitPanels.Length;
    public int TotalSelectedUnits => getTotalSelectedUnits();

    [SerializeField] private MissionUnitUI[] unitPanels = Array.Empty<MissionUnitUI>();

    public int[] GetAllSelectedUnits()
    {
        int[] _units = new int[UnitPanelCount];

        for (int i = 0; i < _units.Length; i++)
        {
            _units[i] = GetSelectedUnitsInPanel(i);
        }

        return _units;
    }
    
    public void SetPanelName(int _id, string _name)
    {
        unitPanels[_id].SetUnitName(_name);
    }

    public void SetPanelMaxUnits(int _id, int _max)
    {
        unitPanels[_id].SetMaxUnitsAvailable(_max);
    }
    
    private int getTotalSelectedUnits()
    {
        int _total = 0; 
        
        foreach (MissionUnitUI _missionUnitUI in unitPanels)
        {
            _total += _missionUnitUI.CurrentlyChosen;
        }

        return _total;
    }

    public void ResetValues()
    {
        foreach (MissionUnitUI _missionUnitUI in unitPanels)
        {
            _missionUnitUI.ResetCounter();
        }
    }
    
    public int GetSelectedUnitsInPanel(int _id)
    {
        return unitPanels[_id].CurrentlyChosen;
    }

}
