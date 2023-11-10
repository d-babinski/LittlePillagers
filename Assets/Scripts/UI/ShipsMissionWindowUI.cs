using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipsMissionWindowUI : MonoBehaviour
{
    public event Action OnShipsChosen = null; 
    public int TotalCapacity => getTotalShipCapacity();
    public int NumberOfPanels => unitSelectionPanel.UnitPanelCount;
    
    [SerializeField] private UnitsSelectionPanelUI unitSelectionPanel = null;
    [SerializeField] private TextMeshProUGUI soldierCapacityText = null;
    [SerializeField] private ImageButton sendButton = null;
    [SerializeField] private SidePanel panelController = null;

    private int[] shipCapacities = Array.Empty<int>();
    private UnitTemplate[] ships = Array.Empty<UnitTemplate>();

    private void Awake()
    {
        sendButton.OnButtonClicked += tryConfirmingShips;
        unitSelectionPanel.OnValueChanged += updateSoldierCapacityText;
    }
    
    public void OpenWindow()
    {
        panelController.Open();
    }

    public void CloseWindow()
    {
        panelController.Close();
    }

    public void SetShipCapacities(int[] _capacities)
    {
        shipCapacities = _capacities;
        updateSoldierCapacityText();
    }

    public void ResetSelection()
    {
        unitSelectionPanel.ResetValues();
        updateSoldierCapacityText();
    }

    private void updateSoldierCapacityText()
    {
        soldierCapacityText.text = getTotalShipCapacity().ToString();
    }
    
    private int getTotalShipCapacity()
    {
        int _totalCapacity = 0;
        
        for (int i = 0; i < unitSelectionPanel.UnitPanelCount; i++)
        {
            _totalCapacity += unitSelectionPanel.GetSelectedUnitsInPanel(i)*getSingleShipCapacity(i);
        }

        return _totalCapacity;
    }
    
    private int getSingleShipCapacity(int _id)
    {
        if (_id < 0 || _id >= shipCapacities.Length)
        {
            return 0;
        }

        return shipCapacities[_id];
    }

    private void tryConfirmingShips()
    {
        if (isChoiceValid() == false)
        {
            return;
        }
        
        OnShipsChosen?.Invoke();
    }

    private bool isChoiceValid()
    {
        return unitSelectionPanel.TotalSelectedUnits > 0;
    }
    
    public void SetClosed()
    {
       panelController.CloseInstantly();
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
    
    public int[] GetChosenQuantities()
    {
        return unitSelectionPanel.GetAllSelectedUnits();
    }
    
    public void SetUnitData(UnitTemplate[] _ships)
    {
        ships = _ships;
        
        for (int i = 0; i < NumberOfPanels; i++)
        {
            if (i >= ships.Length)
            {
                return;
            }
            
            unitSelectionPanel.SetPanelName(i,_ships[i].UnitName);
        }
        
        updateSoldierCapacityText();
    }
}
