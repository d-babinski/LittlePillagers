using UnityEngine;

public class ManagementWindow : MonoBehaviour
{
    [SerializeField] private UnitManagementWindow shipWindow = null;
    [SerializeField] private UnitManagementWindow soldierWindow = null;

    

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
