using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAvailabilitySectionUI : MonoBehaviour
{
    [SerializeField] private UnitCountText unitAvailabilityValueText = null;

    public void SetUnityAvailability(int _available, int _total)
    {
        unitAvailabilityValueText.UpdateCount(_available, _total);
    }
}
