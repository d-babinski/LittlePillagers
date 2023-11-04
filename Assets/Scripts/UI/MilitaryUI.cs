using UnityEngine;

public class MilitaryUI : MonoBehaviour
{
    [SerializeField] private UnitAvailabilitySectionUI soldierSection = null;
    [SerializeField] private UnitAvailabilitySectionUI shipSection = null;

    public void SetShipAvailability(int _available, int _total)
    {
        shipSection.SetUnityAvailability(_available, _total);
    }

    public void SetSoldierAvailability(int _available, int _total)
    {
        soldierSection.SetUnityAvailability(_available, _total);
    }
}
