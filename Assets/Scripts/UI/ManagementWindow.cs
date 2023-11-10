using UnityEngine;

public class ManagementWindow : MonoBehaviour
{
    [SerializeField] private UnitManagementWindow shipWindow = null;
    [SerializeField] private UnitManagementWindow soldierWindow = null;
    [SerializeField] private ImageButton shipPanelButton = null;
    [SerializeField] private ImageButton soldierPanelButton = null;
    
    private void Awake()
    {
        shipPanelButton.OnButtonClicked += ToggleShipWindow;
        soldierPanelButton.OnButtonClicked += ToggleSoldierWindow;
    }

    private void OnDestroy()
    {
        shipPanelButton.OnButtonClicked -= ToggleShipWindow;
        soldierPanelButton.OnButtonClicked -= ToggleSoldierWindow;
    }

    public void ToggleShipWindow()
    {
        shipWindow.Toggle();
    }

    public void ToggleSoldierWindow()
    {
        soldierWindow.Toggle();
    }

    public void CloseAllWindows()
    {
        shipWindow.Close();
        soldierWindow.Close();
    }

    public void CloseAllWindowswithoutTransition()
    {
        shipWindow.CloseWithoutTransition();
        soldierWindow.CloseWithoutTransition();
    }
}
