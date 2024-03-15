using UnityEngine;
using UnityEngine.UI;

public class PortraitFromUnitVariable : MonoBehaviour
{
    [SerializeField] private Image imageToSet = null;
    [SerializeField] private UnitTypeVariable unitToGrabImageFrom = null;

    private UnitType cachedUnit = null;

    private void Update()
    {
        if (unitToGrabImageFrom == null)
        {
            return;
        }
        
        if (cachedUnit != unitToGrabImageFrom.Value)
        {
            cachedUnit = unitToGrabImageFrom.Value;
            imageToSet.sprite = unitToGrabImageFrom.Value.PreviewIcon;
        }
    }
}
